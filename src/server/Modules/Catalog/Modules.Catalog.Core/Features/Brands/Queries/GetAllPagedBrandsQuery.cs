using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Brands;
using MediatR;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Queries
{
    public class GetAllPagedBrandsQuery : IRequest<PaginatedResult<GetAllPagedBrandsResponse>>
    {
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public string[] OrderBy { get; private set; }
        public string SearchString { get; private set; }
    }
}