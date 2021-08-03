using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.People.Core.Features.CartItems.Events
{
    public class CartItemRemovedEvent : Event
    {
        public Guid Id { get; }

        public CartItemRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
            RelatedEntities = new[] { typeof(CartItem) };
        }
    }
}