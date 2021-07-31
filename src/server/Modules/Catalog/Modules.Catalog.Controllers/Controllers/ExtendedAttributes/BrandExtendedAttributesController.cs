using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Filters;
using FluentPOS.Shared.Infrastructure.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Controllers.ExtendedAttributes
{
    [Route(BaseController.BasePath + "/" + nameof(Brand) + "/attributes")]
    internal sealed class BrandExtendedAttributesController : ExtendedAttributesController<Guid, Brand>
    {
        private IMediator _mediatorInstance;
        protected override IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

        [Authorize(Policy = Permissions.BrandsExtendedAttributes.ViewAll)]
        public override Task<IActionResult> GetAllAsync(PaginatedExtendedAttributeFilter<Guid, Brand> filter)
        {
            return base.GetAllAsync(filter);
        }

        [Authorize(Policy = Permissions.BrandsExtendedAttributes.View)]
        public override Task<IActionResult> GetByIdAsync(Guid id, bool bypassCache)
        {
            return base.GetByIdAsync(id, bypassCache);
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