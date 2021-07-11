using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Brands;
using MediatR;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Queries
{
    public class GetAllPagedBrandsQuery : IRequest<PaginatedResult<GetAllPagedBrandsResponse>>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public string[] OrderBy { get; set; }
        public string SearchString { get; }

        public GetAllPagedBrandsQuery(int pageNumber, int pageSize, string searchString, string orderBy)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
            if (!string.IsNullOrWhiteSpace(orderBy))
            {
                OrderBy = orderBy.Split(',');
            }
        }
    }
}