using FluentPOS.Modules.Catalog.Core.Constants;
using FluentPOS.Shared.Application.Queries;
using FluentPOS.Shared.Application.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Categories;
using MediatR;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<Result<GetCategoryByIdResponse>>, ICacheable
    {
        public Guid Id { get; }
        public bool BypassCache { get; }
        public string CacheKey => CatalogCacheKeys.GetCategoryByIdCacheKey(Id);
        public TimeSpan? SlidingExpiration { get; }

        public GetCategoryByIdQuery(Guid categoryId, bool bypassCache = false, TimeSpan? slidingExpiration = null)
        {
            Id = categoryId;
            BypassCache = bypassCache;
            SlidingExpiration = slidingExpiration;
        }
    }
}