using FluentPOS.Modules.People.Core.Entities;
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

namespace FluentPOS.Modules.People.Controllers.ExtendedAttributes
{
    [ApiVersion("1")]
    [Route(BaseController.BasePath + "/" + nameof(Customer) + "/attributes")]
    internal sealed class CustomerExtendedAttributesController : ExtendedAttributesController<Guid, Customer>
    {
        private IMediator _mediatorInstance;
        protected override IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

        [Authorize(Policy = Permissions.CustomersExtendedAttributes.ViewAll)]
        public override Task<IActionResult> GetAllAsync(PaginatedExtendedAttributeFilter<Guid, Customer> filter)
        {
            return base.GetAllAsync(filter);
        }

        [Authorize(Policy = Permissions.CustomersExtendedAttributes.View)]
        public override Task<IActionResult> GetByIdAsync(Guid id, bool bypassCache)
        {
            return base.GetByIdAsync(id, bypassCache);
        }

        [Authorize(Policy = Permissions.CustomersExtendedAttributes.Add)]
        public override Task<IActionResult> CreateAsync(AddExtendedAttributeCommand<Guid, Customer> command)
        {
            return base.CreateAsync(command);
        }

        [Authorize(Policy = Permissions.CustomersExtendedAttributes.Update)]
        public override Task<IActionResult> UpdateAsync(UpdateExtendedAttributeCommand<Guid, Customer> command)
        {
            return base.UpdateAsync(command);
        }

        [Authorize(Policy = Permissions.CustomersExtendedAttributes.Remove)]
        public override Task<IActionResult> RemoveAsync(Guid id)
        {
            return base.RemoveAsync(id);
        }
    }
}