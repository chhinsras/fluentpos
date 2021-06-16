using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using MediatR;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Queries
{
    public class GetAllPagedProductsQuery : IRequest<PaginatedResult<GetAllPagedProductsResponse>>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public string SearchString { get; }

        public GetAllPagedProductsQuery(int pageNumber, int pageSize, string searchString)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
        }
    }
}