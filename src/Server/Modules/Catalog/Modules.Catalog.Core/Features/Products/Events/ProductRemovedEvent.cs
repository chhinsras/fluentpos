using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Events
{
    public class ProductRemovedEvent : Event
    {
        public Guid Id { get; }

        public ProductRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}