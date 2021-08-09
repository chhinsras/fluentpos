// --------------------------------------------------------------------------------------------------
// <copyright file="CartItemsController.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Features.CartItems.Commands;
using FluentPOS.Modules.People.Core.Features.CartItems.Queries;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.DTOs.People.CartItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.People.Controllers
{
    [ApiVersion("1")]
    internal sealed class CartItemsController : BaseController
    {
        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.CartItems.View)]
        public async Task<IActionResult> GetCartItemByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, CartItem> filter)
        {
            var request = Mapper.Map<GetCartItemByIdQuery>(filter);
            var cartItem = await Mediator.Send(request);
            return Ok(cartItem);
        }

        /// <summary>
        /// Gets all Cart Items for a particular CartId.
        /// </summary>
        [HttpGet]
        [Authorize(Policy = Permissions.CartItems.ViewAll)]
        public async Task<IActionResult> GetCartItemsAsync([FromQuery] PaginatedCartItemFilter filter)
        {
            var request = Mapper.Map<GetCartItemsQuery>(filter);
            var cartItems = await Mediator.Send(request);
            return Ok(cartItems);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.CartItems.Add)]
        public async Task<IActionResult> AddCartItemAsync(AddCartItemCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Policy = Permissions.CartItems.Update)]
        public async Task<IActionResult> UpdateCartItemAsync(UpdateCartItemCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.CartItems.Remove)]
        public async Task<IActionResult> RemoveCartItemAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveCartItemCommand(id)));
        }
    }
}