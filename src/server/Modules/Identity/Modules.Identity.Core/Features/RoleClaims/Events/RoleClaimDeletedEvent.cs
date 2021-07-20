using System;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Identity.Core.Features.RoleClaims.Events
{
    public class RoleClaimDeletedEvent : Event
    {
        public int Id { get; }

        public RoleClaimDeletedEvent(int id)
        {
            Id = id;
            AggregateId = Guid.NewGuid();
        }
    }
}