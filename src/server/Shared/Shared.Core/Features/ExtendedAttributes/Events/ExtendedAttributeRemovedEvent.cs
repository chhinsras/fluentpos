using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Events
{
    public class ExtendedAttributeRemovedEvent<TEntity> : Event
    {
        public Guid Id { get; }
        public string EntityName { get; set; }

        public ExtendedAttributeRemovedEvent(Guid id)
        {
            Id = id;
            AggregateId = id;
            EntityName = typeof(TEntity).Name;
            RelatedEntities = new[] { typeof(TEntity) };
        }
    }
}