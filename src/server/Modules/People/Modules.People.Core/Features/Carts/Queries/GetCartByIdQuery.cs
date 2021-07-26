using System;
using FluentPOS.Modules.People.Core.Constants;
using FluentPOS.Shared.Core.Queries;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Carts;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Carts.Queries
{
    public class GetCartByIdQuery : IRequest<Result<GetCartByIdResponse>>, ICacheable
    {
        public Guid Id { get; }
        public bool BypassCache { get; }
        public string CacheKey => PeopleCacheKeys.GetCartByIdCacheKey(Id);
        public TimeSpan? SlidingExpiration { get; }

        public GetCartByIdQuery(Guid cartId, bool bypassCache = false, TimeSpan? slidingExpiration = null)
        {
            Id = cartId;
            BypassCache = bypassCache;
            SlidingExpiration = slidingExpiration;
        }
    }
}