// --------------------------------------------------------------------------------------------------
// <copyright file="CartCreatedEvent.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.People.Core.Features.Carts.Events
{
    public class CartCreatedEvent : Event
    {
        public Guid Id { get; }

        public Guid CustomerId { get; }

        public new DateTime Timestamp { get; }

        public CartCreatedEvent(Cart cart)
        {
            CustomerId = cart.CustomerId;
            Timestamp = cart.Timestamp;
            Id = cart.Id;
            AggregateId = cart.Id;
            RelatedEntities = new[] { typeof(Cart) };
        }
    }
}