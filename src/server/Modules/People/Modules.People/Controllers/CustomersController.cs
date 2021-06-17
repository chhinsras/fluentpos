using FluentPOS.Modules.People.Core.Features.Customers.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Controllers
{
    internal class CustomersController : BaseController
    {
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(RegisterCustomerCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCustomerCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveCustomerCommand(id)));
        }
    }
}