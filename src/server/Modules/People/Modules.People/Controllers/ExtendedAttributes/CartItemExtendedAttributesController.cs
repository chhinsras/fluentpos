// --------------------------------------------------------------------------------------------------
// <copyright file="CartItemExtendedAttributesController.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Filters;
using FluentPOS.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.People.Controllers.ExtendedAttributes
{
    [ApiVersion("1")]
    [Route(BaseController.BasePath + "/" + nameof(CartItem) + "/attributes")]
    internal sealed class CartItemExtendedAttributesController : ExtendedAttributesController<Guid, CartItem>
    {
        [Authorize(Policy = Permissions.CartItemsExtendedAttributes.ViewAll)]
        public override Task<IActionResult> GetAllAsync(PaginatedExtendedAttributeFilter<Guid, CartItem> filter)
        {
            return base.GetAllAsync(filter);
        }

        [Authorize(Policy = Permissions.CartItemsExtendedAttributes.View)]
        public override Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, ExtendedAttribute<Guid, CartItem>> filter)
        {
            return base.GetByIdAsync(filter);
        }

        [Authorize(Policy = Permissions.CartItemsExtendedAttributes.Add)]
        public override Task<IActionResult> CreateAsync(AddExtendedAttributeCommand<Guid, CartItem> command)
        {
            return base.CreateAsync(command);
        }

        [Authorize(Policy = Permissions.CartItemsExtendedAttributes.Update)]
        public override Task<IActionResult> UpdateAsync(UpdateExtendedAttributeCommand<Guid, CartItem> command)
        {
            return base.UpdateAsync(command);
        }

        [Authorize(Policy = Permissions.CartItemsExtendedAttributes.Remove)]
        public override Task<IActionResult> RemoveAsync(Guid id)
        {
            return base.RemoveAsync(id);
        }
    }
}