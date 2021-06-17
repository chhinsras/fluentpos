using FluentPOS.Modules.People.Core.Constants;
using FluentPOS.Shared.Core.Queries;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Customers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries
{
    public class GetCustomerByIdQuery : IRequest<Result<GetCustomerByIdResponse>>, ICacheable
    {
        public Guid Id { get; }
        public bool BypassCache { get; }
        public string CacheKey => PeopleCacheKeys.GetCustomerByIdCacheKey(Id);
        public TimeSpan? SlidingExpiration { get; }

        public GetCustomerByIdQuery(Guid categoryId, bool bypassCache = false, TimeSpan? slidingExpiration = null)
        {
            Id = categoryId;
            BypassCache = bypassCache;
            SlidingExpiration = slidingExpiration;
        }
    }
}
