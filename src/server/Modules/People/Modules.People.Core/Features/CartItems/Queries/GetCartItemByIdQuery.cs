using System;
using FluentPOS.Modules.People.Core.Constants;
using FluentPOS.Shared.Core.Queries;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.CartItems;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.CartItems.Queries
{
    public class GetCartItemByIdQuery : IRequest<Result<GetCartItemByIdResponse>>, ICacheable
    {
        public Guid Id { get; }
        public bool BypassCache { get; }
        public string CacheKey => PeopleCacheKeys.GetCartItemByIdCacheKey(Id);
        public TimeSpan? SlidingExpiration { get; }

        public GetCartItemByIdQuery(Guid cartItemId, bool bypassCache = false, TimeSpan? slidingExpiration = null)
        {
            Id = cartItemId;
            BypassCache = bypassCache;
            SlidingExpiration = slidingExpiration;
        }
    }
}