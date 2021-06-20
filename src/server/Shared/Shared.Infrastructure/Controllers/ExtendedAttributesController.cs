using System;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries;
using FluentPOS.Shared.DTOs.ExtendedAttributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Shared.Infrastructure.Controllers
{
    [ApiController]
    public abstract class ExtendedAttributesController<TEntity> : ControllerBase
        where TEntity : BaseEntity
    {
        protected abstract IMediator Mediator { get; }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll([FromQuery] PaginatedExtendedAttributeFilter filter)
        {
            var extendedAttributes = await Mediator.Send(new GetAllPagedExtendedAttributesQuery<TEntity>(filter.PageNumber, filter.PageSize, filter.SearchString, filter.EntityId, filter.Type));
            return Ok(extendedAttributes);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(Guid id, bool bypassCache)
        {
            var extendedAttribute = await Mediator.Send(new GetExtendedAttributeByIdQuery<TEntity>(id, bypassCache));
            return Ok(extendedAttribute);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(AddExtendedAttributeCommand<TEntity> command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update(UpdateExtendedAttributeCommand<TEntity> command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveExtendedAttributeCommand<TEntity>(id)));
        }
    }
}