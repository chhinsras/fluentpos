// --------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributesController.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Features.Common.Filters;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Filters;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Shared.Infrastructure.Controllers
{
    [ApiController]
    public abstract class ExtendedAttributesController<TEntityId, TEntity> : CommonBaseController
        where TEntity : class, IEntity<TEntityId>
    {
        [HttpGet]
        public virtual async Task<IActionResult> GetAllAsync([FromQuery] PaginatedExtendedAttributeFilter<TEntityId, TEntity> filter)
        {
            var extendedAttributes = await Mediator.Send(new GetExtendedAttributesQuery<TEntityId, TEntity>(filter));
            return Ok(extendedAttributes);
        }

        [HttpGet("{id:guid}")]
        public virtual async Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, ExtendedAttribute<TEntityId, TEntity>> filter)
        {
            var request = Mapper.Map<GetExtendedAttributeByIdQuery<TEntityId, TEntity>>(filter);
            var extendedAttribute = await Mediator.Send(request);
            return Ok(extendedAttribute);
        }

        [HttpPost]
        public virtual async Task<IActionResult> CreateAsync(AddExtendedAttributeCommand<TEntityId, TEntity> command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public virtual async Task<IActionResult> UpdateAsync(UpdateExtendedAttributeCommand<TEntityId, TEntity> command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> RemoveAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveExtendedAttributeCommand<TEntityId, TEntity>(id)));
        }
    }
}