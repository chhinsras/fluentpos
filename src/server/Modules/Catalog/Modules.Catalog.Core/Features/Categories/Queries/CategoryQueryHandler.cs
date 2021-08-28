// --------------------------------------------------------------------------------------------------
// <copyright file="CategoryQueryHandler.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Queries
{
    internal class CategoryQueryHandler :
        IRequestHandler<GetCategoriesQuery, PaginatedResult<GetCategoriesResponse>>,
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

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<PaginatedResult<GetCategoriesResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            Expression<Func<Category, GetCategoriesResponse>> expression = e => new GetCategoriesResponse(e.Id, e.Name, e.Detail);

            var queryable = _context.Categories.AsNoTracking().AsQueryable();

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.Id);

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                queryable = queryable.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.Detail.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.Id.ToString().ToLower(), $"%{request.SearchString.ToLower()}%"));
            }

            var categoryList = await queryable
            .Select(expression)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            if (categoryList == null)
            {
                throw new CatalogException(_localizer["Brands Not Found!"], HttpStatusCode.NotFound);
            }

            return _mapper.Map<PaginatedResult<GetCategoriesResponse>>(categoryList);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<GetCategoryByIdResponse>> Handle(GetCategoryByIdQuery query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var category = await _context.Categories.AsNoTracking().Where(c => c.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
            if (category == null)
            {
                throw new CatalogException(_localizer["Category Not Found!"], HttpStatusCode.NotFound);
            }

            var mappedCategory = _mapper.Map<GetCategoryByIdResponse>(category);
            return await Result<GetCategoryByIdResponse>.SuccessAsync(mappedCategory);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<string>> Handle(GetCategoryImageQuery request, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            string data = await _context.Categories.AsNoTracking().Where(c => c.Id == request.Id).Select(a => a.ImageUrl).FirstOrDefaultAsync(cancellationToken);
            return await Result<string>.SuccessAsync(data: data);
        }
    }
}