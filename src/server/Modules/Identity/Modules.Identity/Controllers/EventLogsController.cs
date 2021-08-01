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
        [Authorize(Policy = Permissions.EventLogs.View)]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginatedEventLogsFilter filter)
        {
            var logs = await _eventLog.Get(filter.PageNumber,filter.PageSize);
            return Ok(logs);
        }
    }
}