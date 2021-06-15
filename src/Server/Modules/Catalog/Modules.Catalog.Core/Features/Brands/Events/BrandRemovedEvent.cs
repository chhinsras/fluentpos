using FluentPOS.Shared.Application.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Events
{
    public class BrandRemovedEvent : Event
    {

        public Guid Id { get; set; }

        public BrandRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
        }
    }
}
