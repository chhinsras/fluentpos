using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Events
{
    public class BrandRemovedEvent : Event
    {
        public Guid Id { get; }

        public BrandRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}