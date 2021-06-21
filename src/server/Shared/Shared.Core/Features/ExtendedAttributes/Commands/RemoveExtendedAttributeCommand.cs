using System;
using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands
{
    public class RemoveExtendedAttributeCommand<TEntityId, TEntity> : IRequest<Result<Guid>>
        where TEntity : class, IEntity<TEntityId>
    {
        public Guid Id { get; }

        public RemoveExtendedAttributeCommand(Guid entityExtendedAttributeId)
        {
            Id = entityExtendedAttributeId;
        }
    }
}