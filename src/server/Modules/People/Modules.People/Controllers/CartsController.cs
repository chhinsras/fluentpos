using System;
using System.Threading.Tasks;
using FluentPOS.Modules.People.Core.Features.Carts.Commands;
using FluentPOS.Modules.People.Core.Features.Carts.Queries;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.DTOs.People.Carts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.People.Controllers
{
    internal sealed class CartsController : BaseController
    {
        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.Carts.View)]
        public async Task<IActionResult> GetByIdAsync(Guid id, bool bypassCache)
        {
            var cart = await Mediator.Send(new GetCartByIdQuery(id, bypassCache));
            return Ok(cart);
        }

        [HttpGet]
        [Authorize(Policy = Permissions.Carts.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedCartFilter filter)
        {
            var request = Mapper.Map<GetAllPagedCartsQuery>(filter);
            var carts = await Mediator.Send(request);
            return Ok(carts);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Carts.Create)]
        public async Task<IActionResult> CreateAsync(CreateCartCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.Carts.Remove)]
        public async Task<IActionResult> RemoveAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveCartCommand(id)));
        }
    }
}