using System.Threading.Tasks;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Wrapper;

namespace FluentPOS.Shared.Core.Interfaces.Services
{
    public interface IEventLogService
    {
        Task<PaginatedResult<EventLog>> Get(int pageNumber, int pageSize);
    }
}