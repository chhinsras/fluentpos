using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Queries
{
    internal class CategoryQueryHandler :
        IRequestHandler<GetAllPagedCategoriesQuery, PaginatedResult<GetAllPagedCategoriesResponse>>,
        IRequestHandler<GetCategoryByIdQuery, Result<GetCategoryByIdResponse>>,
        IRequestHandler<GetCategoryImageQuery, Result<string>>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<CategoryQueryHandler> _localizer;

        public CategoryQueryHandler(ICatalogDbContext context, IMapper mapper, IStringLocalizer<CategoryQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedResult<GetAllPagedCategoriesResponse>> Handle(GetAllPagedCategoriesQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Category, GetAllPagedCategoriesResponse>> expression = e => new GetAllPagedCategoriesResponse(e.Id, e.Name, e.Detail);

            var queryable = _context.Categories.AsQueryable();

            if (request.OrderBy?.Any() == true)
            {
                var ordering = string.Join(",", request.OrderBy);
                queryable = queryable.OrderBy(ordering);
            }
            else
            {
                queryable = queryable.OrderBy(a => a.Id);
            }

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                queryable = queryable.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.Detail.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.Id.ToString().ToLower(), $"%{request.SearchString.ToLower()}%"));
            }
            var categoryList = await queryable
            .Select(expression)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            if (categoryList == null) throw new CatalogException(_localizer["Brands Not Found!"], HttpStatusCode.NotFound);
            var mappedCategories = _mapper.Map<PaginatedResult<GetAllPagedCategoriesResponse>>(categoryList);
            return mappedCategories;
        }

        public async Task<Result<GetCategoryByIdResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.Where(c => c.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
            if (category == null) throw new CatalogException(_localizer["Category Not Found!"], HttpStatusCode.NotFound);
            var mappedCategory = _mapper.Map<GetCategoryByIdResponse>(category);
            return await Result<GetCategoryByIdResponse>.SuccessAsync(mappedCategory);
        }

        public async Task<Result<string>> Handle(GetCategoryImageQuery request, CancellationToken cancellationToken)
        {
            var data = await _context.Categories.Where(c => c.Id == request.Id).Select(a => a.ImageUrl).FirstOrDefaultAsync(cancellationToken);
            return await Result<string>.SuccessAsync(data: data);
        }
    }
}