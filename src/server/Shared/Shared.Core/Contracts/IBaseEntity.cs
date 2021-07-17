using FluentPOS.Shared.Core.Domain;
using System.Collections.Generic;

namespace FluentPOS.Shared.Core.Contracts
{
    public interface IBaseEntity
    {
        public IReadOnlyCollection<Event> DomainEvents { get; }

        public void AddDomainEvent(Event domainEvent);

        public void RemoveDomainEvent(Event domainEvent);

        public void ClearDomainEvents();
    }
}