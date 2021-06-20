using System;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands
{
    public class RemoveExtendedAttributeCommand<TEntity> : IRequest<Result<Guid>>
        where TEntity : BaseEntity
    {
        public Guid Id { get; }

        public RemoveExtendedAttributeCommand(Guid entityExtendedAttributeId)
        {
            Id = entityExtendedAttributeId;
        }
    }
}