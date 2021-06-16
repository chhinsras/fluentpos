using FluentPOS.Shared.Application.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Categories;
using MediatR;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Queries
{
    public class GetAllPagedCategoriesQuery : IRequest<PaginatedResult<GetAllPagedCategoriesResponse>>
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public string SearchString { get; }

        public GetAllPagedCategoriesQuery(int pageNumber, int pageSize, string searchString)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchString = searchString;
        }
    }
}