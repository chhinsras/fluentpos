using FluentPOS.Shared.Core.Queries;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using MediatR;
using System;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Constants;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Queries
{
    public class GetProductByIdQuery : IRequest<Result<GetProductByIdResponse>>, ICacheable
    {
        public Guid Id { get; protected set; }
        public bool BypassCache { get; protected set; }
        public string CacheKey { get; protected set; }
        public TimeSpan? SlidingExpiration { get; protected set; }

        public GetProductByIdQuery()
        {
        }

        public GetProductByIdQuery(Guid productId, bool bypassCache = false, TimeSpan? slidingExpiration = null)
        {
            Id = productId;
            BypassCache = bypassCache;
            CacheKey = CacheKeys.Common.GetEntityByIdCacheKey<Guid,Product>(productId);
            SlidingExpiration = slidingExpiration;
        }
    }
}