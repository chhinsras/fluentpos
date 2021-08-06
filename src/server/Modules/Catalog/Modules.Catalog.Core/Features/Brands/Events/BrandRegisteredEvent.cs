using FluentPOS.Shared.Core.Domain;
using System;
using FluentPOS.Modules.Catalog.Core.Entities;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Events
{
    public class BrandRegisteredEvent : Event
    {
        public Guid Id { get; }
        public string Name { get; }
        public string ImageUrl { get; }
        public string Detail { get; }

        public BrandRegisteredEvent(Brand brand)
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