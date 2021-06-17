using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Core.Features.Commands
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
