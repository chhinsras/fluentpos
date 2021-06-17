using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Events
{
    public class CategoryRemovedEvent : Event
    {
        public Guid Id { get; }

        public CategoryRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}