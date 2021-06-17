using FluentPOS.Shared.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
