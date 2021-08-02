using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Shared.Core.EventLogging
{
    public class EventLog : Event
    {
        public EventLog(Event theEvent, string data, (string oldValues, string newValues) changes, string email, Guid userId)
        {
            Id = Guid.NewGuid();
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = data;
            Email = email;
            OldValues = changes.oldValues;
            NewValues = changes.newValues;
            UserId = userId;
        }

        protected EventLog()
        {
        }

        public Guid Id { get; private set; }

        public string Data { get; private set; }
        public string OldValues { get; private set; }
        public string NewValues { get; private set; }

        public string Email { get; private set; }
        public Guid UserId { get; private set; }
    }
}