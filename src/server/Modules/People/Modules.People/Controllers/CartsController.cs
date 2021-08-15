// --------------------------------------------------------------------------------------------------
// <copyright file="CartsController.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Features.Carts.Commands;
using FluentPOS.Modules.People.Core.Features.Carts.Queries;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.DTOs.People.Carts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.People.Controllers
{
    [ApiVersion("1")]
    internal sealed class CartsController : BaseController
    {
        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Carts.View)]
        public async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, Cart> filter)
        {
            var request = Mapper.Map<GetCartByIdQuery>(filter);
            var cart = await Mediator.Send(request);
            return Ok(cart);
        }

        [HttpGet]
        [Authorize(Policy = Permissions.Carts.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedCartFilter filter)
        {
            var request = Mapper.Map<GetCartsQuery>(filter);
            var carts = await Mediator.Send(request);
            return Ok(carts);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Carts.Create)]
        public async Task<IActionResult> CreateAsync(CreateCartCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Carts.Remove)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveCartCommand(id)));
        }

        [HttpDelete("clear/{id}")]
        [Authorize(Policy = Permissions.Carts.Remove)]
        public async Task<IActionResult> ClearAsync(Guid id)
        {
            return Ok(await Mediator.Send(new ClearCartCommand(id)));
        }
    }
}