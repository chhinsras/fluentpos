using FluentPOS.Shared.Application.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Brands;
using MediatR;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Queries
{
    public class GetAllPagedBrandsQuery : IRequest<PaginatedResult<GetAllPagedBrandsResponse>>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public string SearchString { get; }

        public GetAllPagedBrandsQuery(int pageNumber, int pageSize, string searchString)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
        }
    }
}