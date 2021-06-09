using AutoMapper;
using FluentPOS.Modules.Catalog.Infrastructure.Persistence;
using FluentPOS.Shared.Abstractions.Wrapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalogs.Infrastructure.Features.Brands.Queries.GetAll
{
    public class GetAllBrandsQuery : IRequest<Result<List<GetAllBrandsResponse>>>
    {
        public GetAllBrandsQuery()
        {
        }
    }

    internal class GetAllBrandsCachedQueryHandler : IRequestHandler<GetAllBrandsQuery, Result<List<GetAllBrandsResponse>>>
    {
        private readonly CatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllBrandsCachedQueryHandler> _localizer;

        public GetAllBrandsCachedQueryHandler(CatalogDbContext context, IMapper mapper, IStringLocalizer<GetAllBrandsCachedQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<List<GetAllBrandsResponse>>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brandList = await _context.Brands.ToListAsync();
            if (brandList == null) return await Result<List<GetAllBrandsResponse>>.FailAsync(_localizer["Brand Not Found!"]);
            // TODO: Cache
            var mappedBrands = _mapper.Map<List<GetAllBrandsResponse>>(brandList);
            return await Result<List<GetAllBrandsResponse>>.SuccessAsync(mappedBrands);
        }
    }
}
