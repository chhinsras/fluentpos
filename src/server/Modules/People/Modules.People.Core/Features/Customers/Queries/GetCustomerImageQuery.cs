using FluentPOS.Shared.Core.Wrapper;
using MediatR;
using System;

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