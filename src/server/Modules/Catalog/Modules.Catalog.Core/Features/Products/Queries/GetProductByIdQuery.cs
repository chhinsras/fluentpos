using FluentPOS.Modules.Catalog.Core.Constants;
using FluentPOS.Shared.Application.Queries;
using FluentPOS.Shared.Application.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using MediatR;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Queries
{
    public class GetProductByIdQuery : IRequest<Result<GetProductByIdResponse>>, ICacheable
    {
        public Guid Id { get; }
        public bool BypassCache { get; }
        public string CacheKey => CatalogCacheKeys.GetProductByIdCacheKey(Id);
        public TimeSpan? SlidingExpiration { get; }

        public GetProductByIdQuery(Guid productId, bool bypassCache = false, TimeSpan? slidingExpiration = null)
        {
            Id = productId;
            BypassCache = bypassCache;
            SlidingExpiration = slidingExpiration;
        }
    }
}