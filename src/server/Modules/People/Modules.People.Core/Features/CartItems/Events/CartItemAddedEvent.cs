using System;
using FluentPOS.Modules.People.Core.Entities;
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

        public CartItemAddedEvent(CartItem cartItem)
        {
            CartId = cartItem.CartId;
            ProductId = cartItem.ProductId;
            Quantity = cartItem.Quantity;
            Id = cartItem.Id;
            AggregateId = cartItem.Id;
            RelatedEntities = new[] { typeof(CartItem) };
            Description = $"Added {cartItem.Quantity} Item(s) to cart.";
        }
    }
}