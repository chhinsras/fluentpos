// --------------------------------------------------------------------------------------------------
// <copyright file="EventLogsController.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.DTOs.Identity.EventLogs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FluentPOS.Modules.Identity.Controllers
{
    [ApiVersion("1")]
    internal sealed class EventLogsController : BaseController
    {
        private readonly IEventLogService _eventLog;

        public EventLogsController(IEventLogService eventLog)
        {
            _eventLog = eventLog;
        }

        [HttpGet]
        [Authorize(Policy = Permissions.EventLogs.ViewAll)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedEventLogsFilter filter)
        {
            var request = Mapper.Map<GetEventLogsRequest>(filter);
            var eventLogs = await _eventLog.GetAllAsync(request);
            return Ok(eventLogs);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> LogCustomEventAsync(LogEventRequest request)
        {
            var result = await _eventLog.LogCustomEventAsync(request);
            return Ok(result);
        }
    }
}