using FluentPOS.Shared.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Events
{
    public class CategoryUpdatedEvent : Event
    {
        public CategoryUpdatedEvent(Guid id, string name, string imageUrl, string detail)
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
