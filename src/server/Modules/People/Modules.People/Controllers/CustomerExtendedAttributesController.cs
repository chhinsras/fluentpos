using System;
using System.Threading.Tasks;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands;
using FluentPOS.Shared.DTOs.ExtendedAttributes;
using FluentPOS.Shared.Infrastructure.Controllers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.People.Controllers
{
    [Route(BaseController.BasePath + "/" + nameof(Customer) + "/attributes")]
    public class CustomerExtendedAttributesController : ExtendedAttributesController<Customer>
    {
        private IMediator _mediatorInstance;
        protected override IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

        [Authorize(Policy = Permissions.CustomersExtendedAttributes.ViewAll)]
        public override Task<IActionResult> GetAll(PaginatedExtendedAttributeFilter filter)
        {
            return base.GetAll(filter);
        }

        [Authorize(Policy = Permissions.CustomersExtendedAttributes.View)]
        public override Task<IActionResult> GetById(Guid id, bool bypassCache)
        {
            return base.GetById(id, bypassCache);
        }

        [Authorize(Policy = Permissions.CustomersExtendedAttributes.Add)]
        public override Task<IActionResult> Create(AddExtendedAttributeCommand<Customer> command)
        {
            return base.Create(command);
        }

        [Authorize(Policy = Permissions.CustomersExtendedAttributes.Update)]
        public override Task<IActionResult> Update(UpdateExtendedAttributeCommand<Customer> command)
        {
            return base.Update(command);
        }

        [Authorize(Policy = Permissions.CustomersExtendedAttributes.Remove)]
        public override Task<IActionResult> Remove(Guid id)
        {
            return base.Remove(id);
        }
    }
}