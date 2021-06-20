using System;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Queries;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.ExtendedAttributes;
using MediatR;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries
{
    public class GetExtendedAttributeByIdQuery<TEntity> : IRequest<Result<GetExtendedAttributeByIdResponse>>, ICacheable
        where TEntity : BaseEntity
    {
        public Guid Id { get; }
        public bool BypassCache { get; }
        public string CacheKey => CacheKeys.GetExtendedAttributeByIdCacheKey(typeof(TEntity).Name, Id);
        public TimeSpan? SlidingExpiration { get; }

        public GetExtendedAttributeByIdQuery(Guid brandId, bool bypassCache = false, TimeSpan? slidingExpiration = null)
        {
            Id = brandId;
            BypassCache = bypassCache;
            SlidingExpiration = slidingExpiration;
        }
    }
}