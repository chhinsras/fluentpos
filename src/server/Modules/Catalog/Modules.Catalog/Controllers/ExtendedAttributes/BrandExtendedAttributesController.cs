// --------------------------------------------------------------------------------------------------
// <copyright file="BrandExtendedAttributesController.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Filters;
using FluentPOS.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.Catalog.Controllers.ExtendedAttributes
{
    [ApiVersion("1")]
    [Route(BaseController.BasePath + "/" + nameof(Brand) + "/attributes")]
    internal sealed class BrandExtendedAttributesController : ExtendedAttributesController<Guid, Brand>
    {
        [Authorize(Policy = Permissions.BrandsExtendedAttributes.ViewAll)]
        public override Task<IActionResult> GetAllAsync(PaginatedExtendedAttributeFilter<Guid, Brand> filter)
        {
            return base.GetAllAsync(filter);
        }

        [Authorize(Policy = Permissions.BrandsExtendedAttributes.View)]
        public override Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, ExtendedAttribute<Guid, Brand>> filter)
        {
            return base.GetByIdAsync(filter);
        }

        [Authorize(Policy = Permissions.BrandsExtendedAttributes.Add)]
        public override Task<IActionResult> CreateAsync(AddExtendedAttributeCommand<Guid, Brand> command)
        {
            return base.CreateAsync(command);
        }

        [Authorize(Policy = Permissions.BrandsExtendedAttributes.Update)]
        public override Task<IActionResult> UpdateAsync(UpdateExtendedAttributeCommand<Guid, Brand> command)
        {
            return base.UpdateAsync(command);
        }

        [Authorize(Policy = Permissions.BrandsExtendedAttributes.Remove)]
        public override Task<IActionResult> RemoveAsync(Guid id)
        {
            return base.RemoveAsync(id);
        }
    }
}