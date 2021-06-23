using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.Core.Settings;
using FluentPOS.Shared.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FluentPOS.Shared.Infrastructure.Persistence
{
    internal class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        private readonly PersistenceSettings _persistenceOptions;

        protected string Schema => "Application";

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IOptions<PersistenceSettings> persistenceOptions) : base(options)
        {
            _persistenceOptions = persistenceOptions.Value;
        }

        public DbSet<EventLog> EventLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(Schema);
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyApplicationConfiguration(_persistenceOptions);
        }
    }
}