using System;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Identity.Core.Features.Users.Events
{
    public class UserDeletedEvent : Event
    {
        public string Id { get; }

        public UserDeletedEvent(string id)
        {
            Id = id;
            AggregateId = Guid.TryParse(id, out var aggregateId)
                ? aggregateId
                : Guid.NewGuid();
        }
    }
}