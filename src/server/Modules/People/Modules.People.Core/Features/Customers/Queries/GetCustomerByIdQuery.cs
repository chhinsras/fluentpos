using FluentPOS.Shared.Core.Queries;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Customers;
using MediatR;
using System;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries
{
    public class GetCustomerByIdQuery : IRequest<Result<GetCustomerByIdResponse>>, ICacheable
    {
        public Guid Id { get; protected set; }
        public bool BypassCache { get; protected set; }
        public string CacheKey { get; protected set; }
        public TimeSpan? SlidingExpiration { get; protected set; }
    }
}