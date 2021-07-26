using System;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.CartItems.Commands
{
    public class RemoveCartItemCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; }

        public RemoveCartItemCommand(Guid cartItemId)
        {
            Id = cartItemId;
        }
    }
}