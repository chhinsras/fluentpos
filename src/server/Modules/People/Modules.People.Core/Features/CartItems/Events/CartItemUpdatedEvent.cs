using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Modules.People.Core.Features.CartItems.Commands;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.People.Core.Features.CartItems.Events
{
    public class CartItemUpdatedEvent : Event
    {
        public Guid Id { get; set; }

        public Guid CartId { get; }

        public Guid ProductId { get; }

        public int Quantity { get; set; }

        public CartItemUpdatedEvent(UpdateCartItemCommand command)
        {
            CartId = command.CartId;
            ProductId = command.ProductId;
            Quantity = command.Quantity;
            Id = command.Id;
            AggregateId = command.Id;
            RelatedEntities = new[] { typeof(CartItem) };
        }
    }
}