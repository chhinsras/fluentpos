using System;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Events
{
    public class ExtendedAttributeRemovedEvent : Event
    {
        public Guid Id { get; }

        public ExtendedAttributeRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}