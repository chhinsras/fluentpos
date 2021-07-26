using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Exceptions;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Queries
{
    internal class ProductQueryHandler :
        IRequestHandler<GetAllPagedProductsQuery, PaginatedResult<GetAllPagedProductsResponse>>,
        IRequestHandler<GetProductByIdQuery, Result<GetProductByIdResponse>>,
        IRequestHandler<GetProductImageQuery, Result<string>>
    {
        private readonly ICatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<ProductQueryHandler> _localizer;

        public ProductQueryHandler(ICatalogDbContext context, IMapper mapper, IStringLocalizer<ProductQueryHandler> localizer)
        {
            _context = context;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task<PaginatedResult<GetAllPagedProductsResponse>> Handle(GetAllPagedProductsQuery request, CancellationToken cancellationToken)
        {
            var queryable = _context.Products.ProjectTo<GetAllPagedProductsResponse>(_mapper.ConfigurationProvider).OrderBy(x => x.Id).AsQueryable();

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
                || EF.Functions.Like(x.LocaleName.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.Detail.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.BarcodeSymbology.ToLower(), $"%{request.SearchString.ToLower()}%")
                || EF.Functions.Like(x.Id.ToString().ToLower(), $"%{request.SearchString.ToLower()}%"));
            }

            var productList = await queryable
                .AsNoTracking()
                .ToPaginatedListAsync(request.PageNumber, request.PageSize);

            if (productList == null) throw new CatalogException(_localizer["Products Not Found!"], HttpStatusCode.NotFound);

            var mappedProducts = _mapper.Map<PaginatedResult<GetAllPagedProductsResponse>>(productList);

            return mappedProducts;
        }

        public async Task<Result<GetProductByIdResponse>> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await _context.Products.Where(p => p.Id == query.Id).FirstOrDefaultAsync(cancellationToken);
            if (product == null) throw new CatalogException(_localizer["Product Not Found!"], HttpStatusCode.NotFound);
            var mappedProduct = _mapper.Map<GetProductByIdResponse>(product);
            return await Result<GetProductByIdResponse>.SuccessAsync(mappedProduct);
        }

        public async Task<Result<string>> Handle(GetProductImageQuery query, CancellationToken cancellationToken)
        {
            var data = await _context.Products.Where(p => p.Id == query.Id).Select(x => x.ImageUrl).FirstOrDefaultAsync(cancellationToken);
            return await Result<string>.SuccessAsync(data: data);
        }
    }
}