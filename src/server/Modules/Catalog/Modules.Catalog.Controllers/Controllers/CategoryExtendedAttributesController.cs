using System;
using System.Threading.Tasks;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands;
using FluentPOS.Shared.DTOs.ExtendedAttributes;
using FluentPOS.Shared.Infrastructure.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.Catalog.Controllers
{
    [Route(BaseController.BasePath + "/" + nameof(Category) + "/attributes")]
    public class CategoryExtendedAttributesController : ExtendedAttributesController<Guid, Category>
    {
        private IMediator _mediatorInstance;
        protected override IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

        [Authorize(Policy = Permissions.CategoriesExtendedAttributes.ViewAll)]
        public override Task<IActionResult> GetAll(PaginatedExtendedAttributeFilter<Guid> filter)
        {
            return base.GetAll(filter);
        }

        [Authorize(Policy = Permissions.CategoriesExtendedAttributes.View)]
        public override Task<IActionResult> GetById(Guid id, bool bypassCache)
        {
            return base.GetById(id, bypassCache);
        }

        [Authorize(Policy = Permissions.CategoriesExtendedAttributes.Add)]
        public override Task<IActionResult> Create(AddExtendedAttributeCommand<Guid, Category> command)
        {
            return base.Create(command);
        }

        [Authorize(Policy = Permissions.CategoriesExtendedAttributes.Update)]
        public override Task<IActionResult> Update(UpdateExtendedAttributeCommand<Guid, Category> command)
        {
            return base.Update(command);
        }

        [Authorize(Policy = Permissions.CategoriesExtendedAttributes.Remove)]
        public override Task<IActionResult> Remove(Guid id)
        {
            return base.Remove(id);
        }
    }
}