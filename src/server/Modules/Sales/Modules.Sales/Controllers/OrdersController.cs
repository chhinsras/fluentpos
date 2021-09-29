// --------------------------------------------------------------------------------------------------
// <copyright file="OrdersController.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentPOS.Modules.Sales.Core.Features.Sales.Commands;
using FluentPOS.Modules.Sales.Core.Features.Sales.Queries;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.DTOs.Sales.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.Sales.Controllers
{
    [ApiVersion("1")]
    internal sealed class OrdersController : BaseController
    {

        [HttpGet]
        [Authorize(Policy = Permissions.Sales.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedSalesFilter filter)
        {
            var request = Mapper.Map<GetSalesQuery>(filter);
            var sales = await Mediator.Send(request);
            return Ok(sales);
        }

        [HttpPost]
        [Authorize(Policy = Permissions.Sales.Register)]
        public async Task<IActionResult> RegisterAsync(RegisterSaleCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
    }
}