using FluentPOS.Modules.Catalog.Core.Constants;
using FluentPOS.Shared.Core.Queries;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Brands;
using MediatR;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Queries
{
    public class GetBrandByIdQuery : IRequest<Result<GetBrandByIdResponse>>, ICacheable
    {
        public Guid Id { get; }
        public bool BypassCache { get; }
        public string CacheKey => CatalogCacheKeys.GetBrandByIdCacheKey(Id);
        public TimeSpan? SlidingExpiration { get; }

        public GetBrandByIdQuery(Guid brandId, bool bypassCache = false, TimeSpan? slidingExpiration = null)
        {
            Id = brandId;
            BypassCache = bypassCache;
            SlidingExpiration = slidingExpiration;
        }
    }
}