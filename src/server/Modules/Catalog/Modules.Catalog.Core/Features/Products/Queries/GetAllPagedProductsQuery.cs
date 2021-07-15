using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using MediatR;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Queries
{
    public class GetAllPagedProductsQuery : IRequest<PaginatedResult<GetAllPagedProductsResponse>>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public string SearchString { get; }
        public Guid BrandId { get; }
        public Guid CategoryId { get; }
        public string[] OrderBy { get; set; }

        public GetAllPagedProductsQuery(int pageNumber, int pageSize, string searchString, Guid brandId, Guid categoryId, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            BrandId = brandId;
            CategoryId = categoryId;
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                OrderBy = orderBy.Split(',');
            }
        }
    }
}