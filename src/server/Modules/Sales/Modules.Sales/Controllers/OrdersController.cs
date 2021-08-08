using System.Threading.Tasks;
using FluentPOS.Modules.Sales.Core.Features.Sales.Commands;
using FluentPOS.Shared.Core.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.Sales.Controllers
{
    [ApiVersion("1")]
    internal sealed class OrdersController : BaseController
    {
        [HttpPost]
        [Authorize(Policy = Permissions.Sales.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterSaleCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}