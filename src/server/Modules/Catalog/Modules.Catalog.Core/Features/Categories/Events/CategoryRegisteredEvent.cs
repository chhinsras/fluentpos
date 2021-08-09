// --------------------------------------------------------------------------------------------------
// <copyright file="CategoryRegisteredEvent.cs" company="FluentPOS">
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
    public class CategoryRegisteredEvent : Event
    {
        public Guid Id { get; }

        public string Name { get; }

        public string ImageUrl { get; }

        public string Detail { get; }

        public CategoryRegisteredEvent(Category category)
        {
            Name = category.Name;
            ImageUrl = category.ImageUrl;
            Detail = category.Detail;
            Id = category.Id;
            AggregateId = category.Id;
            RelatedEntities = new[] { typeof(Category) };
        }
    }
}