using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Constants;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Shared.Application.Queries;
using FluentPOS.Shared.Application.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Brands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Queries
{
    public class GetBrandByIdQuery : IRequest<Result<GetBrandByIdResponse>>, ICacheable
    {
        public Guid Id { get; set; }

        public bool BypassCache { get; set; }

        public string CacheKey => CatalogCacheKeys.GetBrandByIdCacheKey(Id);

        public TimeSpan? SlidingExpiration { get; set; }
    }
}