using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using System;

namespace FluentPOS.Modules.People.Core.Features.Customers.Commands
{
    public class RemoveCustomerCommand : IRequest<Result<Guid>>
    {
        public Guid Id { get; }

        public RemoveCustomerCommand(Guid customerId)
        {
            Id = customerId;
        }
    }
}