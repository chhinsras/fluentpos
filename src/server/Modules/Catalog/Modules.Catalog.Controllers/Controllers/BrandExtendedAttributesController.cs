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
    [Route(BaseController.BasePath + "/" + nameof(Brand) + "/attributes")]
    public class BrandExtendedAttributesController : ExtendedAttributesController<Brand>
    {
        private IMediator _mediatorInstance;
        protected override IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

        //[Authorize(Policy = Permissions.BrandExtendedAttributes.View)]
        public override Task<IActionResult> GetAll(PaginatedExtendedAttributeFilter filter)
        {
            return base.GetAll(filter);
        }

        //[Authorize(Policy = Permissions.BrandExtendedAttributes.View)]
        public override Task<IActionResult> GetById(Guid id, bool bypassCache)
        {
            return base.GetById(id, bypassCache);
        }

        //[Authorize(Policy = Permissions.BrandExtendedAttributes.Create)]
        public override Task<IActionResult> Create(AddExtendedAttributeCommand<Brand> command)
        {
            return base.Create(command);
        }

        //[Authorize(Policy = Permissions.BrandExtendedAttributes.Update)]
        public override Task<IActionResult> Update(UpdateExtendedAttributeCommand<Brand> command)
        {
            return base.Update(command);
        }

        //[Authorize(Policy = Permissions.BrandExtendedAttributes.Delete)]
        public override Task<IActionResult> Delete(Guid id)
        {
            return base.Delete(id);
        }
    }
}