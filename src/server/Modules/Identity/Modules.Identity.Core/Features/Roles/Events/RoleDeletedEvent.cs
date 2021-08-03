using System;
using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Identity.Core.Features.Roles.Events
{
    public class RoleDeletedEvent : Event
    {
        public string Id { get; }

        public RoleDeletedEvent(string id)
        {
            Id = id;
            AggregateId = Guid.TryParse(id, out var aggregateId)
                ? aggregateId
                : Guid.NewGuid();
            RelatedEntities = new[] { typeof(FluentRole) };
        }
    }
}