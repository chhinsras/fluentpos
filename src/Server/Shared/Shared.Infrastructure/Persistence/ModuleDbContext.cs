using FluentPOS.Shared.Application.Domain;
using FluentPOS.Shared.Application.EventLogging;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Shared.Infrastructure.Persistence
{
    public abstract class ModuleDbContext : DbContext
    {
        private readonly IMediator _mediator;
        private readonly IEventLogger _eventLogger;

        public ModuleDbContext(DbContextOptions options, IMediator mediator, IEventLogger eventLogger) : base(options)
        {
            _mediator = mediator;
            _eventLogger = eventLogger;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var domainEntities = this.ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await _eventLogger.Save(domainEvent);
                    await _mediator.Publish(domainEvent);
                });
            await Task.WhenAll(tasks);
            return (await base.SaveChangesAsync(true, cancellationToken));
        }
    }
}