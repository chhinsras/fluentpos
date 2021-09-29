using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Modules.Sales.Core.Abstractions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Sales.Orders;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Sales.Core.Features.Sales.Queries
{
    internal class SalesQueryHandler :
                IRequestHandler<GetSalesQuery, PaginatedResult<GetSalesResponse>>
    {
        private readonly ISalesDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SalesQueryHandler> _localizer;

        public SalesQueryHandler(
            ISalesDbContext context,
            IMapper mapper,
            IStringLocalizer<SalesQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedResult<GetSalesResponse>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.Orders.AsNoTracking()
                .ProjectTo<GetSalesResponse>(_mapper.ConfigurationProvider)
                .OrderBy(x => x.TimeStamp)
                .AsQueryable();

            // string ordering = new OrderByConverter().Convert(request.OrderBy);
            // queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.TimeStamp);

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                queryable = queryable.Where(x => EF.Functions.Like(x.ReferenceNumber.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.Id.ToString().ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.CustomerId.ToString().ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.CustomerName.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.CustomerEmail.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.CustomerPhone.ToString().ToLower(), $"%{request.SearchString.ToLower()}%"));
            }

            var saleList = await queryable
                .AsNoTracking()
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            if (saleList == null)
            {
                throw new SalesException(_localizer["Sales Not Found!"], HttpStatusCode.NotFound);
            }

            return _mapper.Map<PaginatedResult<GetSalesResponse>>(saleList);

        }
    }
}