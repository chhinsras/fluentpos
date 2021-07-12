using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Categories;
using MediatR;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Queries
{
    public class GetAllPagedCategoriesQuery : IRequest<PaginatedResult<GetAllPagedCategoriesResponse>>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public string SearchString { get; }
        public string[] OrderBy { get; set; }

        public GetAllPagedCategoriesQuery(int pageNumber, int pageSize, string searchString, string orderBy)
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