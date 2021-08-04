using System;
using FluentPOS.Shared.Core.Queries;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.CartItems;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.CartItems.Queries
{
    public class GetCartItemByIdQuery : IRequest<Result<GetCartItemByIdResponse>>, ICacheable
    {
        public Guid Id { get; protected set; }
        public bool BypassCache { get; protected set; }
        public string CacheKey { get; protected set; }
        public TimeSpan? SlidingExpiration { get; protected set; }
    }
}