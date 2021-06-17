using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Events
{
    public class CategoryRegisteredEvent : Event
    {
        public CategoryRegisteredEvent(Guid id, string name, string imageUrl, string detail)
        {
            Name = name;
            ImageUrl = imageUrl;
            Detail = detail;
            Id = id;
            AggregateId = id;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string ImageUrl { get; }
        public string Detail { get; }
    }
}