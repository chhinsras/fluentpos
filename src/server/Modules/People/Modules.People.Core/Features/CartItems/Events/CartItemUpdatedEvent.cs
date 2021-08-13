// --------------------------------------------------------------------------------------------------
// <copyright file="CartItemUpdatedEvent.cs" company="FluentPOS">
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
    public class CartItemUpdatedEvent : Event
    {
        public Guid Id { get; set; }

        public Guid CartId { get; }

        public Guid ProductId { get; }

        public int Quantity { get; set; }

        public CartItemUpdatedEvent(CartItem command)
        {
            CartId = command.CartId;
            ProductId = command.ProductId;
            Quantity = command.Quantity;
            Id = command.Id;
            AggregateId = command.Id;
            RelatedEntities = new[] { typeof(CartItem) };
            EventDescription = "Updated Cart Item.";
        }
    }
}