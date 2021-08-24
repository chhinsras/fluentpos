// --------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributeRemovedEvent.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Utilities;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Events
{
    public class ExtendedAttributeRemovedEvent<TEntity> : Event
    {
        public Guid Id { get; }

        public string EntityName { get; set; }

        public ExtendedAttributeRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
            EntityName = typeof(TEntity).GetGenericTypeName();
            RelatedEntities = new[] { typeof(TEntity) };
        }
    }
}