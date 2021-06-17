using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Modules.People.Core.Features.Customers.Events
{
    public class CustomerRemovedEvent : Event
    {
        public Guid Id { get; }

        public CustomerRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}