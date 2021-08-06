using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.People.Core.Features.Carts.Events
{
    public class CartRemovedEvent : Event
    {
        public Guid Id { get; }

        public CartRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
            RelatedEntities = new[] { typeof(Cart) };
        }
    }
}