using FluentPOS.Shared.Application.EventLogging;
using FluentPOS.Shared.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Shared.Infrastructure.Persistence
{
    class ApplicationDbContext : DbContext,IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<EventLog> EventLogs { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Application");
            base.OnModelCreating(modelBuilder);
        }

    }
}
