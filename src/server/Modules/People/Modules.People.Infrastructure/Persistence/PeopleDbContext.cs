using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Settings;
using FluentPOS.Shared.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FluentPOS.Modules.People.Infrastructure.Persistence
{
    public class PeopleDbContext : ModuleDbContext, IPeopleDbContext
    {
        private readonly PersistenceSettings _persistenceOptions;

        public PeopleDbContext(
            DbContextOptions<PeopleDbContext> options,
            IMediator mediator,
            IEventLogger eventLogger,
            IOptions<PersistenceSettings> persistenceOptions)
                : base(options, mediator, eventLogger, persistenceOptions)
        {
            _persistenceOptions = persistenceOptions.Value;
        }

        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("People");
            base.OnModelCreating(modelBuilder);
        }
    }
}