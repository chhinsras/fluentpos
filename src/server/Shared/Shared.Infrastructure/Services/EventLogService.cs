using System.Linq;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.Wrapper;

namespace FluentPOS.Shared.Infrastructure.Services
{
    public class EventLogService : IEventLogService
    {
        private readonly IApplicationDbContext _dbContext;

        public EventLogService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PaginatedResult<EventLog>> Get(int pageNumber, int pageSize)
        {
            return await _dbContext.EventLogs
            .OrderByDescending(a => a.Timestamp)
            .ToPaginatedListAsync(pageNumber, pageSize);
        }
    }
}