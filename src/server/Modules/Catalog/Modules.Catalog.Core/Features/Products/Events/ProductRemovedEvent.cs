using FluentPOS.Shared.Core.Domain;
using System;
using FluentPOS.Modules.Catalog.Core.Entities;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Events
{
    public class ProductRemovedEvent : Event
    {
        public Guid Id { get; }

        public ProductRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
            RelatedEntities = new[] { typeof(Product) };
        }
    }
}