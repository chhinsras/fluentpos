// --------------------------------------------------------------------------------------------------
// <copyright file="UserDeletedEvent.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Identity.Core.Features.Users.Events
{
    public class UserDeletedEvent : Event
    {
        public string Id { get; }

        public UserDeletedEvent(string id)
        {
            Id = id;
            AggregateId = Guid.TryParse(id, out var aggregateId)
                ? aggregateId
                : Guid.NewGuid();
            RelatedEntities = new[] { typeof(FluentUser) };
        }
    }
}