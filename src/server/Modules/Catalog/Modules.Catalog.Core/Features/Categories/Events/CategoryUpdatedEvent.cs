using FluentPOS.Shared.Core.Domain;
using System;
using FluentPOS.Modules.Catalog.Core.Entities;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Events
{
    public class CategoryUpdatedEvent : Event
    {
        public Guid Id { get; }
        public string Name { get; }
        public string ImageUrl { get; }
        public string Detail { get; }

        public CategoryUpdatedEvent(Category category)
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