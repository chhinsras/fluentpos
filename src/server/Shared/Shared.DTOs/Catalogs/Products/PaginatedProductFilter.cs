using FluentPOS.Shared.DTOs.Filters;
using System;

namespace FluentPOS.Shared.DTOs.Catalogs.Products
{
    public class PaginatedProductFilter : PaginatedFilter
    {
        public string SearchString { get; set; }
        public Guid[] BrandIds { get; set; }
        public Guid[] CategoryIds { get; set; }
    }
}