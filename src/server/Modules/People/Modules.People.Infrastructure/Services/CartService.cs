// --------------------------------------------------------------------------------------------------
// <copyright file="CartService.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentPOS.Modules.People.Core.Features.Carts.Commands;
using FluentPOS.Modules.People.Core.Features.Carts.Queries;
using FluentPOS.Shared.Core.IntegrationServices.People;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Carts;
using MediatR;

namespace FluentPOS.Modules.People.Infrastructure.Services
{
    public class CartService : ICartService
    {
        private readonly IMediator _mediator;

        public CartService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Result<GetCartByIdResponse>> GetDetailsAsync(Guid cartId)
        {
            return await _mediator.Send(new GetCartByIdQuery(cartId, true));
        }

        public async Task<Result<Guid>> RemoveCartAsync(Guid cartId)
        {
            return await _mediator.Send(new RemoveCartCommand(cartId));
        }
    }
}