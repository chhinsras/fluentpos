using System;
using System.Threading.Tasks;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Filters;
using FluentPOS.Shared.Infrastructure.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.People.Controllers.ExtendedAttributes
{
    [ApiVersion("1")]
    [Route(BaseController.BasePath + "/" + nameof(Cart) + "/attributes")]
    internal sealed class CartExtendedAttributesController : ExtendedAttributesController<Guid, Cart>
    {
        private IMediator _mediatorInstance;
        protected override IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

        [Authorize(Policy = Permissions.CartsExtendedAttributes.ViewAll)]
        public override Task<IActionResult> GetAllAsync(PaginatedExtendedAttributeFilter<Guid, Cart> filter)
        {
            return base.GetAllAsync(filter);
        }

        [Authorize(Policy = Permissions.CartsExtendedAttributes.View)]
        public override Task<IActionResult> GetByIdAsync(Guid id, bool bypassCache)
        {
            return base.GetByIdAsync(id, bypassCache);
        }

        [Authorize(Policy = Permissions.CartsExtendedAttributes.Add)]
        public override Task<IActionResult> CreateAsync(AddExtendedAttributeCommand<Guid, Cart> command)
        {
            return base.CreateAsync(command);
        }

        [Authorize(Policy = Permissions.CartsExtendedAttributes.Update)]
        public override Task<IActionResult> UpdateAsync(UpdateExtendedAttributeCommand<Guid, Cart> command)
        {
            return base.UpdateAsync(command);
        }

        [Authorize(Policy = Permissions.CartsExtendedAttributes.Remove)]
        public override Task<IActionResult> RemoveAsync(Guid id)
        {
            return base.RemoveAsync(id);
        }
    }
}