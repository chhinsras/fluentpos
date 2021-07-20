using System;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.DTOs.Identity.Roles;

namespace FluentPOS.Modules.Identity.Core.Features.Roles.Events
{
    public class RoleAddedEvent : Event
    {
        public RoleAddedEvent(RoleRequest request)
        {
            Name = request.Name;
            Description = request.Description;
            Id = request.Id;
            if (Guid.TryParse(request.Id, out var aggregateId))
            {
                AggregateId = aggregateId;
            }
        }

        public string Id { get; }
        public string Name { get; }
        public string Description { get; }
    }
}