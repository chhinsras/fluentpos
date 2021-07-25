using FluentPOS.Modules.People.Core.Features.Customers.Commands;
using FluentPOS.Modules.People.Core.Features.Customers.Queries;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.DTOs.People.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Controllers
{
    internal sealed class CustomersController : BaseController
    {
        [HttpGet]
        [Authorize(Policy = Permissions.Customers.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedCustomerFilter filter)
        {
            var customers = await Mediator.Send(new GetAllPagedCustomersQuery(filter.PageNumber, filter.PageSize, filter.SearchString));
            return Ok(customers);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Customers.View)]
        public async Task<IActionResult> GetByIdAsync(Guid id, bool bypassCache)
        {
            var customer = await Mediator.Send(new GetCustomerByIdQuery(id, bypassCache));
            return Ok(customer);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Customers.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterCustomerCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Policy = Permissions.Customers.Update)]
        public async Task<IActionResult> UpdateAsync(UpdateCustomerCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Customers.Remove)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveCustomerCommand(id)));
        }
    }
}