using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Shared.Application.Queries;
using FluentPOS.Shared.Application.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Brands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Queries
{
    public class GetBrandByIdQuery : IRequest<Result<GetBrandByIdResponse>>, ICacheable
    {
        public Guid Id { get; set; }

        public bool BypassCache { get; set; }

        public string CacheKey => $"Brand-{Id}";

        public TimeSpan? SlidingExpiration { get; set; }
    }

    internal class GetBrandByIdQueryHandler : IRequestHandler<GetBrandByIdQuery, Result<GetBrandByIdResponse>>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetBrandByIdQueryHandler> _localizer;

        public GetBrandByIdQueryHandler(ICatalogDbContext context, IMapper mapper, IStringLocalizer<GetBrandByIdQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<GetBrandByIdResponse>> Handle(GetBrandByIdQuery query, CancellationToken cancellationToken)
        {
            var brand = await _context.Brands.SingleOrDefaultAsync(b => b.Id == query.Id);
            if (brand == null) throw new CatalogException(_localizer["Brand Not Found!"]);
            var mappedBrand = _mapper.Map<GetBrandByIdResponse>(brand);
            return await Result<GetBrandByIdResponse>.SuccessAsync(mappedBrand);
        }
    }
}