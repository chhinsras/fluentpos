using AutoMapper;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Exceptions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Customers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Mappings.Converters;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries
{
    internal class CustomerQueryHandler :
        IRequestHandler<GetCustomersQuery, PaginatedResult<GetCustomersResponse>>,
        IRequestHandler<GetCustomerByIdQuery, Result<GetCustomerByIdResponse>>,
        IRequestHandler<GetCustomerImageQuery, Result<string>>
    {
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CustomerQueryHandler> _localizer;

        public CustomerQueryHandler(IPeopleDbContext context, IMapper mapper, IStringLocalizer<CustomerQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedResult<GetCustomersResponse>> Handle(GetCustomersQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Customer, GetCustomersResponse>> expression = e => new GetCustomersResponse(e.Id, e.Name, e.Phone, e.Email, e.ImageUrl, e.Type);

            var queryable = _context.Customers.OrderBy(x => x.Id).AsQueryable();

            var ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.Id);

            if (!string.IsNullOrEmpty(request.SearchString)) queryable = queryable.Where(c => c.Name.Contains(request.SearchString) || c.Phone.Contains(request.SearchString) || c.Email.Contains(request.SearchString));

            var customerList = await queryable
                .Select(expression)
                .AsNoTracking()
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            if (customerList == null) throw new PeopleException(_localizer["Customers Not Found!"], HttpStatusCode.NotFound);

            var mappedCustomers = _mapper.Map<PaginatedResult<GetCustomersResponse>>(customerList);

            return mappedCustomers;
        }

        public async Task<Result<GetCustomerByIdResponse>> Handle(GetCustomerByIdQuery query, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers.Where(c => c.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
            if (customer == null) throw new PeopleException(_localizer["Customer Not Found!"], HttpStatusCode.NotFound);
            var mappedCustomer = _mapper.Map<GetCustomerByIdResponse>(customer);
            return await Result<GetCustomerByIdResponse>.SuccessAsync(mappedCustomer);
        }

        public async Task<Result<string>> Handle(GetCustomerImageQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.Customers.Where(c => c.Id == request.Id).Select(a => a.ImageUrl).FirstOrDefaultAsync(cancellationToken);
            return await Result<string>.SuccessAsync(data: data);
        }
    }
}