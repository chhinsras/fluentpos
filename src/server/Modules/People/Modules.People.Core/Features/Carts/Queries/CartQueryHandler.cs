// --------------------------------------------------------------------------------------------------
// <copyright file="CartQueryHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

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
using FluentPOS.Shared.DTOs.People.Carts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Carts.Queries
{
    internal class CartQueryHandler :
        IRequestHandler<GetCartsQuery, PaginatedResult<GetCartsResponse>>,
        IRequestHandler<GetCartByIdQuery, Result<GetCartByIdResponse>>
    {
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CartQueryHandler> _localizer;

        public CartQueryHandler(
            IPeopleDbContext context,
            IMapper mapper,
            IStringLocalizer<CartQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<PaginatedResult<GetCartsResponse>> Handle(GetCartsQuery request, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            Expression<Func<Cart, GetCartsResponse>> expression = e => new GetCartsResponse(e.Id, e.CustomerId, e.Timestamp);
            var queryable = _context.Carts.AsNoTracking().AsQueryable();

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.Id);

            if (request.CustomerId != null && !request.CustomerId.Equals(Guid.Empty))
            {
                queryable = queryable.Where(x => x.CustomerId.Equals(request.CustomerId));
            }

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                // TODO - add some searching logic if needed
                // queryable = queryable.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{request.SearchString.ToLower()}%")
                // || EF.Functions.Like(x.Detail.ToLower(), $"%{request.SearchString.ToLower()}%")
                // || EF.Functions.Like(x.Id.ToString().ToLower(), $"%{request.SearchString.ToLower()}%"));
            }

            var cartList = await queryable
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            if (cartList == null)
            {
                throw new PeopleException(_localizer["Carts Not Found!"], HttpStatusCode.NotFound);
            }

            return _mapper.Map<PaginatedResult<GetCartsResponse>>(cartList);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<GetCartByIdResponse>> Handle(GetCartByIdQuery query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var cart = await _context.Carts.AsNoTracking()
                .Where(c => c.Id == query.Id)
                .Include(a => a.CartItems)
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(cancellationToken);

            if (cart == null)
            {
                throw new PeopleException(_localizer["Cart Not Found!"], HttpStatusCode.NotFound);
            }

            var mappedCart = _mapper.Map<GetCartByIdResponse>(cart);
            return await Result<GetCartByIdResponse>.SuccessAsync(mappedCart);
        }
    }
}