using FluentPOS.Shared.Core.EventLogging;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Shared.Core.Interfaces
{
    public interface IApplicationDbContext : IDbContext
    {
        public DbSet<EventLog> EventLogs { get; set; }
    }
}