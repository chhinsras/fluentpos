// --------------------------------------------------------------------------------------------------
// <copyright file="CartItemAddedEvent.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.People.Core.Entities;
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
            EventDescription = $"Added {cartItem.Quantity} Item(s) to cart.";
        }
    }
}