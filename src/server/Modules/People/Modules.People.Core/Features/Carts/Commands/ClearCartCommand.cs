using System;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Carts.Commands
{
    public class ClearCartCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; }

        public ClearCartCommand(Guid cartId)
        {
            Id = cartId;
        }
    }
}