using System;
using FluentPOS.Modules.People.Core.Constants;
using FluentPOS.Shared.Core.Queries;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Carts;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Carts.Queries
{
    public class GetCartByCustomerIdQuery : IRequest<Result<GetCartByCustomerIdResponse>>, ICacheable
    {
        public Guid CustomerId { get; }
        public bool BypassCache { get; }
        public string CacheKey => PeopleCacheKeys.GetCartByCustomerIdCacheKey(CustomerId);
        public TimeSpan? SlidingExpiration { get; }

        public GetCartByCustomerIdQuery(Guid customerId, bool bypassCache = false, TimeSpan? slidingExpiration = null)
        {
            CustomerId = customerId;
            BypassCache = bypassCache;
            SlidingExpiration = slidingExpiration;
        }
    }
}