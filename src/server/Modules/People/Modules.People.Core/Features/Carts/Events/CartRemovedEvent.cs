// --------------------------------------------------------------------------------------------------
// <copyright file="CartRemovedEvent.cs" company="FluentPOS">
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