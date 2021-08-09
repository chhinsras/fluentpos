// --------------------------------------------------------------------------------------------------
// <copyright file="BrandUpdatedEvent.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Events
{
    public class BrandUpdatedEvent : Event
    {
        public Guid Id { get; }

        public string Name { get; }

        public string ImageUrl { get; }

        public string Detail { get; }

        public BrandUpdatedEvent(Brand brand)
        {
            Name = brand.Name;
            ImageUrl = brand.ImageUrl;
            Detail = brand.Detail;
            Id = brand.Id;
            AggregateId = brand.Id;
            RelatedEntities = new[] { typeof(Brand) };
        }
    }
}