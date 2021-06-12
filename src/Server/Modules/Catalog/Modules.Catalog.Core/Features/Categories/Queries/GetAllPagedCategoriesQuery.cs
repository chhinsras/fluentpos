using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Entites;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Shared.Abstractions.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Categories;
using FluentPOS.Shared.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Queries
{
    public class GetAllPagedCategoriesQuery : IRequest<PaginatedResult<GetAllPagedCategoriesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }

        public GetAllPagedCategoriesQuery(int pageNumber, int pageSize, string searchString)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
        }
    }

    internal class GetAllPagedCategoriesCachedQueryHandler : IRequestHandler<GetAllPagedCategoriesQuery, PaginatedResult<GetAllPagedCategoriesResponse>>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GetAllPagedCategoriesCachedQueryHandler> _localizer;

        public GetAllPagedCategoriesCachedQueryHandler(ICatalogDbContext context, IMapper mapper, IStringLocalizer<GetAllPagedCategoriesCachedQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedResult<GetAllPagedCategoriesResponse>> Handle(GetAllPagedCategoriesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Category, GetAllPagedCategoriesResponse>> expression = e => new GetAllPagedCategoriesResponse(e.Id, e.Name, e.Detail);

            var queryable = _context.Categories.OrderBy(x => x.Id).AsQueryable();

            if (!string.IsNullOrEmpty(request.SearchString)) queryable = queryable.Where(c => c.Name.Contains(request.SearchString) || c.Detail.Contains(request.SearchString));

            var categoryList = await queryable
                .Select(expression)
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            if (categoryList == null) throw new CatalogException(_localizer["Category Not Found!"]);
            // TODO: Cache
            return categoryList;
        }
    }
}