using AutoMapper;
using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Entites;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Shared.Abstractions.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using FluentPOS.Shared.Infrastructure.Extensions;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Queries
{
    public class GetAllPagedProductsQuery : IRequest<PaginatedResult<GetAllPagedProductsResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string SearchString { get; set; }

        public GetAllPagedProductsQuery(int pageNumber, int pageSize, string searchString)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
        }
    }

    internal class GGetAllProductsQueryHandler : IRequestHandler<GetAllPagedProductsQuery, PaginatedResult<GetAllPagedProductsResponse>>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<GGetAllProductsQueryHandler> _localizer;

        public GGetAllProductsQueryHandler(ICatalogDbContext context, IMapper mapper, IStringLocalizer<GGetAllProductsQueryHandler> localizer)
        {
            _context = context;
            _localizer = localizer;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<GetAllPagedProductsResponse>> Handle(GetAllPagedProductsQuery query, CancellationToken cancellationToken)
        {
            Expression<Func<Product, GetAllPagedProductsResponse>> expression = e => new GetAllPagedProductsResponse
            (
                e.Id, 
                e.Name, 
                e.LocaleName, 
                e.BarcodeSymbology, 
                e.Detail, 
                e.BrandId, 
                e.CategoryId, 
                e.Price, 
                e.Cost, 
                e.ImageUrl, 
                e.Tax, 
                e.TaxMethod, 
                e.IsAlert, 
                e.AlertQuantity
            );
            var queryable = _context.Products.OrderBy(x => x.Id).AsQueryable();

            if (!string.IsNullOrEmpty(query.SearchString)) queryable = queryable.Where(p => p.Name.Contains(query.SearchString) || 
                p.BarcodeSymbology.Contains(query.SearchString) || p.Detail.Contains(query.SearchString));

            var productList = await queryable
                .Select(expression)
                .ToPaginatedListAsync(query.PageNumber, query.PageSize);

            if (productList == null) throw new CatalogException(_localizer["Product Not Found!"]);
            // TODO: Cache
            var mappedProducts = _mapper.Map<PaginatedResult<GetAllPagedProductsResponse>>(productList);

            return mappedProducts;
        }
    }
}
