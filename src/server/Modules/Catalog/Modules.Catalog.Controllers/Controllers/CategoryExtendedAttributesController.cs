using System;
using System.Threading.Tasks;
using FluentPOS.Modules.Catalog.Core.Entities;
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
    public class CategoryExtendedAttributesController : ExtendedAttributesController<Category>
    {
        private IMediator _mediatorInstance;
        protected override IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

        //[Authorize(Policy = Permissions.CategoryExtendedAttributes.View)]
        public override Task<IActionResult> GetAll(PaginatedExtendedAttributeFilter filter)
        {
            return base.GetAll(filter);
        }

        //[Authorize(Policy = Permissions.CategoryExtendedAttributes.View)]
        public override Task<IActionResult> GetById(Guid id, bool bypassCache)
        {
            return base.GetById(id, bypassCache);
        }

        //[Authorize(Policy = Permissions.CategoryExtendedAttributes.Create)]
        public override Task<IActionResult> Create(AddExtendedAttributeCommand<Category> command)
        {
            return base.Create(command);
        }

        //[Authorize(Policy = Permissions.CategoryExtendedAttributes.Update)]
        public override Task<IActionResult> Update(UpdateExtendedAttributeCommand<Category> command)
        {
            return base.Update(command);
        }

        //[Authorize(Policy = Permissions.CategoryExtendedAttributes.Delete)]
        public override Task<IActionResult> Delete(Guid id)
        {
            return base.Delete(id);
        }
    }
}