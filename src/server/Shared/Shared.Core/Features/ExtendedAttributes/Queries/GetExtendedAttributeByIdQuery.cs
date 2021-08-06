using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Queries;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.ExtendedAttributes;
using MediatR;
using System;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries
{
    public class GetExtendedAttributeByIdQuery<TEntityId, TEntity> : IRequest<Result<GetExtendedAttributeByIdResponse<TEntityId>>>, ICacheable
        where TEntity : class, IEntity<TEntityId>
    {
        public Guid Id { get; protected set; }
        public bool BypassCache { get; protected set; }
        public string CacheKey { get; protected set; }
        public TimeSpan? SlidingExpiration { get; protected set; }
    }
}