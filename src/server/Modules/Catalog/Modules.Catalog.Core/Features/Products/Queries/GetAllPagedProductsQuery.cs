using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using MediatR;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Queries
{
    public class GetAllPagedProductsQuery : IRequest<PaginatedResult<GetAllPagedProductsResponse>>
    {
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public string SearchString { get; private set; }
        public Guid[] BrandIds { get; private set; }
        public Guid[] CategoryIds { get; private set; }
        public string[] OrderBy { get; private set; }
    }
}