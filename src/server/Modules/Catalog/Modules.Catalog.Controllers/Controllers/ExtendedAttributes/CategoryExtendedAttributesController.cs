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
    [ApiVersion("1")]
    [Route(BaseController.BasePath + "/" + nameof(Category) + "/attributes")]
    internal sealed class CategoryExtendedAttributesController : ExtendedAttributesController<Guid, Category>
    {
        private IMediator _mediatorInstance;
        protected override IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

        [Authorize(Policy = Permissions.CategoriesExtendedAttributes.ViewAll)]
        public override Task<IActionResult> GetAllAsync(PaginatedExtendedAttributeFilter<Guid, Category> filter)
        {
            return base.GetAllAsync(filter);
        }

        [Authorize(Policy = Permissions.CategoriesExtendedAttributes.View)]
        public override Task<IActionResult> GetByIdAsync(Guid id, bool bypassCache)
        {
            return base.GetByIdAsync(id, bypassCache);
        }

        [Authorize(Policy = Permissions.CategoriesExtendedAttributes.Add)]
        public override Task<IActionResult> CreateAsync(AddExtendedAttributeCommand<Guid, Category> command)
        {
            return base.CreateAsync(command);
        }

        [Authorize(Policy = Permissions.CategoriesExtendedAttributes.Update)]
        public override Task<IActionResult> UpdateAsync(UpdateExtendedAttributeCommand<Guid, Category> command)
        {
            return base.UpdateAsync(command);
        }

        [Authorize(Policy = Permissions.CategoriesExtendedAttributes.Remove)]
        public override Task<IActionResult> RemoveAsync(Guid id)
        {
            return base.RemoveAsync(id);
        }
    }
}