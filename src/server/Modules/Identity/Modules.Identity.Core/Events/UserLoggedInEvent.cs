using System;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Identity.Core.Events
{
    public class UserLoggedInEvent: Event
    {
        public Guid UserId { get; }

        public new DateTime Timestamp { get; }

        public UserLoggedInEvent(Guid userId)
        {
            UserId = userId;
            Timestamp = DateTime.Now;
            AggregateId = userId;
        }
    }
}