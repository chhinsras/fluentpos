// --------------------------------------------------------------------------------------------------
// <copyright file="CartCommandHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Exceptions;
using FluentPOS.Modules.People.Core.Features.Carts.Events;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Carts.Commands
{
    internal class CartCommandHandler :
        IRequestHandler<CreateCartCommand, Result<Guid>>,
        IRequestHandler<RemoveCartCommand, Result<Guid>>,
        IRequestHandler<ClearCartCommand, Result<Guid>>
    {
        private readonly IPeopleDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CartCommandHandler> _localizer;
        private readonly IDistributedCache _cache;

        public CartCommandHandler(
            IPeopleDbContext context,
            IMapper mapper,
            IStringLocalizer<CartCommandHandler> localizer,
            IDistributedCache cache)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
            _cache = cache;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(CreateCartCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            if (!await _context.Customers.AnyAsync(p => p.Id == command.CustomerId, cancellationToken))
            {
                throw new PeopleException(_localizer["Customer Not Found!"], HttpStatusCode.NotFound);
            }

            var cart = _mapper.Map<Cart>(command);
            cart.AddDomainEvent(new CartCreatedEvent(cart));
            await _context.Carts.AddAsync(cart, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            return await Result<Guid>.SuccessAsync(cart.Id, _localizer["Cart Created"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(RemoveCartCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(b => b.Id == command.Id, cancellationToken);
            if (cart == null)
            {
                throw new PeopleException(_localizer["Cart Not Found!"], HttpStatusCode.NotFound);
            }

            cart.AddDomainEvent(new CartRemovedEvent(cart.Id));
            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync(cancellationToken);
            await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, Cart>(command.Id), cancellationToken);
            return await Result<Guid>.SuccessAsync(cart.Id, _localizer["Cart Deleted"]);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<Guid>> Handle(ClearCartCommand command, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(b => b.Id == command.Id, cancellationToken);
            if (cart == null)
            {
                throw new PeopleException(_localizer["Cart Not Found!"], HttpStatusCode.NotFound);
            }

            var cartItems = await _context.CartItems.Where(a => a.CartId == command.Id).ToListAsync();
            if (cartItems.Count > 0)
            {
                _context.CartItems.RemoveRange(cartItems);
                await _context.SaveChangesAsync(cancellationToken);
            }

            await _cache.RemoveAsync(CacheKeys.Common.GetEntityByIdCacheKey<Guid, Cart>(command.Id), cancellationToken);
            return await Result<Guid>.SuccessAsync(cart.Id, _localizer["Cart Cleared"]);
        }
    }
}