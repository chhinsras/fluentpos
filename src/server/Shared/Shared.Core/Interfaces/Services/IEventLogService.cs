using System.Threading.Tasks;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Identity.EventLogs;

namespace FluentPOS.Shared.Core.Interfaces.Services
{
    public interface IEventLogService
    {
        Task<PaginatedResult<EventLog>> GetAllAsync(GetEventLogsRequest request);
    }
}