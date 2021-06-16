using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Shared.Core.EventLogging
{
    public class EventLog : Event
    {
        public EventLog(Event theEvent, string data, string user)
        {
            Id = Guid.NewGuid();
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = data;
            User = user;
        }

        protected EventLog()
        {
        }

        public Guid Id { get; private set; }

        public string Data { get; private set; }

        public string User { get; private set; }
    }
}