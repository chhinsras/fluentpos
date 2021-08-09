// --------------------------------------------------------------------------------------------------
// <copyright file="UserLoggedInEvent.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Identity.Core.Events
{
    public class UserLoggedInEvent : Event
    {
        public Guid UserId { get; }

        public new DateTime Timestamp { get; }

        public UserLoggedInEvent(Guid userId)
        {
            UserId = userId;
            Timestamp = DateTime.Now;
            AggregateId = userId;
            RelatedEntities = new[] { typeof(FluentUser) };
        }
    }
}