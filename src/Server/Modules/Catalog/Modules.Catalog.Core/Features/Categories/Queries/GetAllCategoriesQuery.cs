using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Shared.Abstractions.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Queries
{
    public class GetAllCategoriesQuery : IRequest<Result<List<GetAllCategoriesResponse>>>
    {
        public GetAllCategoriesQuery()
        {
        }
    }

    internal class GetAllCategoriesCachedQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result<List<GetAllCategoriesResponse>>>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllCategoriesCachedQueryHandler> _localizer;

        public GetAllCategoriesCachedQueryHandler(ICatalogDbContext context, IMapper mapper, IStringLocalizer<GetAllCategoriesCachedQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<Result<List<GetAllCategoriesResponse>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categoryList = await _context.Categories.ToListAsync();
            if (categoryList == null) throw new CatalogException(_localizer["Category Not Found!"]);
            // TODO: Cache
            var mappedCategories = _mapper.Map<List<GetAllCategoriesResponse>>(categoryList);
            return await Result<List<GetAllCategoriesResponse>>.SuccessAsync(mappedCategories);
        }
    }
}