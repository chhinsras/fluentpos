// --------------------------------------------------------------------------------------------------
// <copyright file="EventLog.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Shared.Core.EventLogging
{
    public class EventLog : Event, IEntity<Guid>
    {
        public EventLog(Event theEvent, string data, (string oldValues, string newValues) changes, string email, Guid userId)
        {
            Id = Guid.NewGuid();
            AggregateId = theEvent.AggregateId;
            MessageType = theEvent.MessageType;
            Data = data;
            Email = email;
            OldValues = changes.oldValues;
            NewValues = changes.newValues;
            UserId = userId;
            EventDescription = theEvent.EventDescription;
        }

        protected EventLog()
        {
        }

        public Guid Id { get; set; }

        public string Data { get; private set; }

        public string OldValues { get; private set; }

        public string NewValues { get; private set; }

        public string Email { get; private set; }

        public Guid UserId { get; private set; }
    }
}