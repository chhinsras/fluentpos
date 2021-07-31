using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Exceptions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.CartItems;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.CartItems.Queries
{
    internal class CartItemQueryHandler :
        IRequestHandler<GetAllPagedCartItemsQuery, PaginatedResult<GetAllPagedCartItemsResponse>>,
        IRequestHandler<GetCartItemByIdQuery, Result<GetCartItemByIdResponse>>
    {
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CartItemQueryHandler> _localizer;

        public CartItemQueryHandler(IPeopleDbContext context, IMapper mapper, IStringLocalizer<CartItemQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedResult<GetAllPagedCartItemsResponse>> Handle(GetAllPagedCartItemsQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<CartItem, GetAllPagedCartItemsResponse>> expression = e => new GetAllPagedCartItemsResponse(e.Id, e.CartId, e.ProductId, e.Quantity);
            var queryable = _context.CartItems.AsQueryable();

            var ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.Id);

            if (request.CartId != null && !request.CartId.Equals(Guid.Empty)) queryable = queryable.Where(x => x.CartId.Equals(request.CartId));
            if (request.ProductId != null && !request.ProductId.Equals(Guid.Empty)) queryable = queryable.Where(x => x.ProductId.Equals(request.ProductId));
            if (!string.IsNullOrEmpty(request.SearchString))
            {
                //TODO - add some searching logic if needed
                //queryable = queryable.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{request.SearchString.ToLower()}%")
                //|| EF.Functions.Like(x.Detail.ToLower(), $"%{request.SearchString.ToLower()}%")
                //|| EF.Functions.Like(x.Id.ToString().ToLower(), $"%{request.SearchString.ToLower()}%"));
            }
            var cartItemList = await queryable
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            if (cartItemList == null) throw new PeopleException(_localizer["Cart Items Not Found!"], HttpStatusCode.NotFound);
            var mappedCartItems = _mapper.Map<PaginatedResult<GetAllPagedCartItemsResponse>>(cartItemList);
            return mappedCartItems;
        }

        public async Task<Result<GetCartItemByIdResponse>> Handle(GetCartItemByIdQuery query, CancellationToken cancellationToken)
        {
            var cartItem = await _context.CartItems.Where(b => b.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
            if (cartItem == null) throw new PeopleException(_localizer["Cart Item Not Found!"], HttpStatusCode.NotFound);
            var mappedCartItem = _mapper.Map<GetCartItemByIdResponse>(cartItem);
            return await Result<GetCartItemByIdResponse>.SuccessAsync(mappedCartItem);
        }
    }
}