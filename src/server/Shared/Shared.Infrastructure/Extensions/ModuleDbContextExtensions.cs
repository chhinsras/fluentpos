// --------------------------------------------------------------------------------------------------
// <copyright file="ModuleDbContextExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using FluentPOS.Shared.Core.Utilities;
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
            List<(EntityEntry entityEntry, string oldValues, string newValues)> changes,
            IJsonSerializer jsonSerializer,
            CancellationToken cancellationToken = new())
                where TModuleDbContext : DbContext, IModuleDbContext
        {
            var domainEntities = context.ChangeTracker
                .Entries<IBaseEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    var relatedEntriesChanges = changes.Where(x => domainEvent.RelatedEntities.Any(t => t == x.entityEntry.Entity.GetType())).ToList();
                    if (relatedEntriesChanges.Any())
                    {
                        var oldValues = relatedEntriesChanges.ToDictionary(x => x.entityEntry.Entity.GetType().GetGenericTypeName(), y => y.oldValues);
                        var newValues = relatedEntriesChanges.ToDictionary(x => x.entityEntry.Entity.GetType().GetGenericTypeName(), y => y.newValues);
                        var relatedChanges = (oldValues.Count == 0 ? null : jsonSerializer.Serialize(oldValues), newValues.Count == 0 ? null : jsonSerializer.Serialize(newValues));
                        await eventLogger.SaveAsync(domainEvent, relatedChanges);
                        await mediator.Publish(domainEvent, cancellationToken);
                    }
                    else
                    {
                        await eventLogger.SaveAsync(domainEvent, (null, null));
                        await mediator.Publish(domainEvent, cancellationToken);
                    }
                });
            await Task.WhenAll(tasks);

            return await context.SaveChangesAsync(true, cancellationToken);
        }

        public static int SaveChangeWithPublishEvents<TModuleDbContext>(
            this TModuleDbContext context,
            IEventLogger eventLogger,
            IMediator mediator,
            List<(EntityEntry entityEntry, string oldValues, string newValues)> changes,
            IJsonSerializer jsonSerializer)
            where TModuleDbContext : DbContext, IModuleDbContext
        {
            return SaveChangeWithPublishEventsAsync(context, eventLogger, mediator, changes, jsonSerializer).GetAwaiter().GetResult();
        }
    }
}