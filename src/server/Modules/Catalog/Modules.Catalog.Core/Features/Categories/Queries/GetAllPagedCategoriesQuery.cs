using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Categories;
using MediatR;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Queries
{
    public class GetAllPagedCategoriesQuery : IRequest<PaginatedResult<GetAllPagedCategoriesResponse>>
    {
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public string SearchString { get; private set; }
        public string[] OrderBy { get; private set; }
    }
}