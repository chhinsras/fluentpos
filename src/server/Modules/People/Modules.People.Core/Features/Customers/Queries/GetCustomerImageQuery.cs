using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries
{
    public class GetCustomerImageQuery : IRequest<Result<string>>
    {
        public Guid Id { get; }

        public GetCustomerImageQuery(Guid customerId)
        {
            Id = customerId;
        }
    }
}
