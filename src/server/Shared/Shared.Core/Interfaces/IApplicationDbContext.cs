using FluentPOS.Shared.Core.EventLogging;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Core.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<EventLog> EventLogs { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}