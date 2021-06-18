using System.Collections.Generic;
using FluentPOS.Shared.Core.Contracts;

namespace FluentPOS.Shared.Core.Domain
{
    public abstract class BaseEntity<TId> : BaseEntity, IEntity<TId>
    {
        public TId Id { get; set; }

        protected BaseEntity()
        {
            Id = GenerateNewId();
        }

        protected abstract TId GenerateNewId();
    }

    public abstract class BaseEntity : IEntity
    {
        private List<Event> _domainEvents;
        public IReadOnlyCollection<Event> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(Event domainEvent)
        {
            _domainEvents ??= new List<Event>();
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(Event domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}