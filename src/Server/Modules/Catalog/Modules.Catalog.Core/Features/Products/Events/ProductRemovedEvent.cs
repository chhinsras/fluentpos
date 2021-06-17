using FluentPOS.Shared.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
