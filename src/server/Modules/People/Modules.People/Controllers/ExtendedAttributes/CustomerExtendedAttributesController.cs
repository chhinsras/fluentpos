using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Filters;
using FluentPOS.Shared.Infrastructure.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Features.Common.Filters;

namespace FluentPOS.Modules.People.Controllers.ExtendedAttributes
{
    [ApiVersion("1")]
    [Route(BaseController.BasePath + "/" + nameof(Customer) + "/attributes")]
    internal sealed class CustomerExtendedAttributesController : ExtendedAttributesController<Guid, Customer>
    {
        [Authorize(Policy = Permissions.CustomersExtendedAttributes.ViewAll)]
        public override Task<IActionResult> GetAllAsync(PaginatedExtendedAttributeFilter<Guid, Customer> filter)
        {
            return base.GetAllAsync(filter);
        }

        [Authorize(Policy = Permissions.CustomersExtendedAttributes.View)]
        public override Task<IActionResult> GetByIdAsync([FromQuery] GetByIdCacheableFilter<Guid, ExtendedAttribute<Guid, Customer>> filter)
        {
            return base.GetByIdAsync(filter);
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