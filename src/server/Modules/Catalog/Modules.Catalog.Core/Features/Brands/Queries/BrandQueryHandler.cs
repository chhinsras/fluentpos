// --------------------------------------------------------------------------------------------------
// <copyright file="BrandQueryHandler.cs" company="FluentPOS">
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
using FluentPOS.Shared.DTOs.Catalogs.Brands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Queries
{
    internal class BrandQueryHandler :
        IRequestHandler<GetBrandsQuery, PaginatedResult<GetBrandsResponse>>,
        IRequestHandler<GetBrandByIdQuery, Result<GetBrandByIdResponse>>,
        IRequestHandler<GetBrandImageQuery, Result<string>>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<BrandQueryHandler> _localizer;

        public BrandQueryHandler(
            ICatalogDbContext context,
            IMapper mapper,
            IStringLocalizer<BrandQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<PaginatedResult<GetBrandsResponse>> Handle(GetBrandsQuery request, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            Expression<Func<Brand, GetBrandsResponse>> expression = e => new GetBrandsResponse(e.Id, e.Name, e.Detail);
            var queryable = _context.Brands.AsNoTracking().AsQueryable();

            string ordering = new OrderByConverter().Convert(request.OrderBy);
            queryable = !string.IsNullOrWhiteSpace(ordering) ? queryable.OrderBy(ordering) : queryable.OrderBy(a => a.Id);

            if (!string.IsNullOrEmpty(request.SearchString))
            {
                queryable = queryable.Where(x => EF.Functions.Like(x.Name.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.Detail.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.Id.ToString().ToLower(), $"%{request.SearchString.ToLower()}%"));
            }

            var brandList = await queryable
            .Select(expression)
            .ToPaginatedListAsync(request.PageNumber, request.PageSize);
            if (brandList == null)
            {
                throw new CatalogException(_localizer["Brands Not Found!"], HttpStatusCode.NotFound);
            }

            return _mapper.Map<PaginatedResult<GetBrandsResponse>>(brandList);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<GetBrandByIdResponse>> Handle(GetBrandByIdQuery query, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            var brand = await _context.Brands.AsNoTracking()
                .Where(b => b.Id == query.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (brand == null)
            {
                throw new CatalogException(_localizer["Brand Not Found!"], HttpStatusCode.NotFound);
            }

            var mappedBrand = _mapper.Map<GetBrandByIdResponse>(brand);
            return await Result<GetBrandByIdResponse>.SuccessAsync(mappedBrand);
        }

#pragma warning disable RCS1046 // Asynchronous method name should end with 'Async'.
        public async Task<Result<string>> Handle(GetBrandImageQuery request, CancellationToken cancellationToken)
#pragma warning restore RCS1046 // Asynchronous method name should end with 'Async'.
        {
            string data = await _context.Brands.AsNoTracking()
                .Where(b => b.Id == request.Id)
                .Select(a => a.ImageUrl)
                .FirstOrDefaultAsync(cancellationToken);

            return await Result<string>.SuccessAsync(data: data);
        }
    }
}