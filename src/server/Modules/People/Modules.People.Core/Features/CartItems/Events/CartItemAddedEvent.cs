using System;
using FluentPOS.Modules.People.Core.Features.CartItems.Commands;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.People.Core.Features.CartItems.Events
{
    public class CartItemAddedEvent : Event
    {
        public Guid Id { get; set; }

        public Guid CartId { get; }

        public Guid ProductId { get; }

        public int Quantity { get; set; }

        public CartItemAddedEvent(Guid id, AddCartItemCommand command)
        {
            CartId = command.CartId;
            ProductId = command.ProductId;
            Quantity = command.Quantity;
            Id = id;
            AggregateId = id;
        }
    }
}