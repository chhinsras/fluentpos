// --------------------------------------------------------------------------------------------------
// <copyright file="CategoryRemovedEvent.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Events
{
    public class CategoryRemovedEvent : Event
    {
        public Guid Id { get; }

        public CategoryRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
            RelatedEntities = new[] { typeof(Category) };
        }
    }
}