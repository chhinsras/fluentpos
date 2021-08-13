// --------------------------------------------------------------------------------------------------
// <copyright file="IBaseEntity.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Shared.Core.Contracts
{
    public interface IBaseEntity
    {
        public IReadOnlyCollection<Event> DomainEvents { get; }

        public void AddDomainEvent(Event domainEvent);

        public void RemoveDomainEvent(Event domainEvent);

        public void ClearDomainEvents();
    }
}