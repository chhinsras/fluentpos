using System;
using System.Threading.Tasks;
using FluentPOS.Modules.People.Core.Features.CartItems.Commands;
using FluentPOS.Modules.People.Core.Features.CartItems.Queries;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.DTOs.People.CartItems;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.People.Controllers
{
    internal sealed class CartItemsController : BaseController
    {
        [HttpGet("{id}")]
        [Authorize(Policy = Permissions.CartItems.View)]
        public async Task<IActionResult> GetCartItemByIdAsync(Guid id, bool bypassCache)
        {
            var cartItem = await Mediator.Send(new GetCartItemByIdQuery(id, bypassCache));
            return Ok(cartItem);
        }

        [HttpGet]
        [Authorize(Policy = Permissions.CartItems.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedCartItemFilter filter)
        {
            var request = Mapper.Map<GetAllPagedCartItemsQuery>(filter);
            var cartItems = await Mediator.Send(request);
            return Ok(cartItems);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.CartItems.Add)]
        public async Task<IActionResult> AddCartItemAsync(AddCartItemCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpPut]
        [Authorize(Policy = Permissions.CartItems.Update)]
        public async Task<IActionResult> UpdateCartItemAsync(UpdateCartItemCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Permissions.CartItems.Remove)]
        public async Task<IActionResult> RemoveCartItemAsync(Guid id)
        {
            return Ok(await Mediator.Send(new RemoveCartItemCommand(id)));
        }
    }
}