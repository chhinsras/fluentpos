using System;
using FluentPOS.Shared.Core.Wrapper;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Carts.Commands
{
    public class CreateCartCommand : IRequest<Result<Guid>>
    {
        public Guid CustomerId { get; set; }
    }
}