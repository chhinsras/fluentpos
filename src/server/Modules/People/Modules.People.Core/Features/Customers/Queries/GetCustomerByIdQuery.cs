using FluentPOS.Modules.People.Core.Constants;
using FluentPOS.Shared.Core.Queries;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Customers;
using MediatR;
using System;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries
{
    public class GetCustomerByIdQuery : IRequest<Result<GetCustomerByIdResponse>>, ICacheable
    {
        public Guid Id { get; }
        public bool BypassCache { get; }
        public string CacheKey => PeopleCacheKeys.GetCustomerByIdCacheKey(Id);
        public TimeSpan? SlidingExpiration { get; }

        public GetCustomerByIdQuery(Guid customerId, bool bypassCache = false, TimeSpan? slidingExpiration = null)
        {
            Id = customerId;
            BypassCache = bypassCache;
            SlidingExpiration = slidingExpiration;
        }
    }
}