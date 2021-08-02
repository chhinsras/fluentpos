using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FluentPOS.Shared.Infrastructure.Extensions
{
    public static class ModuleDbContextExtensions
    {
        public static async Task<int> SaveChangeWithPublishEventsAsync<TModuleDbContext>(
            this TModuleDbContext context,
            IEventLogger eventLogger,
            IMediator mediator,
            (string oldValues,string newValues) changes,
            CancellationToken cancellationToken = new()
            )
                where TModuleDbContext : DbContext, IModuleDbContext
        {
            var domainEntities = context.ChangeTracker
                .Entries<IBaseEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await eventLogger.Save(domainEvent,changes);
                    await mediator.Publish(domainEvent, cancellationToken);
                });
            await Task.WhenAll(tasks);

            return await context.SaveChangesAsync(true, cancellationToken);
        }

        public static int SaveChangeWithPublishEvents<TModuleDbContext>(
            this TModuleDbContext context,
            IEventLogger eventLogger,
            IMediator mediator,
            (string oldValues,string newValues) changes)
            where TModuleDbContext : DbContext, IModuleDbContext
        {
            return SaveChangeWithPublishEventsAsync(context, eventLogger, mediator,changes).GetAwaiter().GetResult();
        }
    }
}