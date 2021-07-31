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
    [Route(BaseController.BasePath + "/" + nameof(Product) + "/attributes")]
    internal sealed class ProductExtendedAttributesController : ExtendedAttributesController<Guid, Product>
    {
        private IMediator _mediatorInstance;
        protected override IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

        [Authorize(Policy = Permissions.ProductsExtendedAttributes.ViewAll)]
        public override Task<IActionResult> GetAllAsync(PaginatedExtendedAttributeFilter<Guid, Product> filter)
        {
            return base.GetAllAsync(filter);
        }

        [Authorize(Policy = Permissions.ProductsExtendedAttributes.View)]
        public override Task<IActionResult> GetByIdAsync(Guid id, bool bypassCache)
        {
            return base.GetByIdAsync(id, bypassCache);
        }

        [Authorize(Policy = Permissions.ProductsExtendedAttributes.Add)]
        public override Task<IActionResult> CreateAsync(AddExtendedAttributeCommand<Guid, Product> command)
        {
            return base.CreateAsync(command);
        }

        [Authorize(Policy = Permissions.ProductsExtendedAttributes.Update)]
        public override Task<IActionResult> UpdateAsync(UpdateExtendedAttributeCommand<Guid, Product> command)
        {
            return base.UpdateAsync(command);
        }

        [Authorize(Policy = Permissions.ProductsExtendedAttributes.Remove)]
        public override Task<IActionResult> RemoveAsync(Guid id)
        {
            return base.RemoveAsync(id);
        }
    }
}