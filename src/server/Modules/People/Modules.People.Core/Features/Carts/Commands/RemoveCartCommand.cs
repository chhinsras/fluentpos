using System;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Carts.Commands
{
    public class RemoveCartCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; }

        public RemoveCartCommand(Guid cartId)
        {
            Id = cartId;
        }
    }
}