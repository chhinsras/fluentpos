using System;

namespace FluentPOS.Modules.Catalog.Core.Constants
{
    public static class CatalogCacheKeys
{
        public static string GetBrandByIdCacheKey(Guid Id) { return $"Brand-{Id}"; }
        public static string GetCategoryByIdCacheKey(Guid Id) { return $"Category-{Id}"; }
        public static string GetProductByIdCacheKey(Guid Id) { return $"Product-{Id}"; }    
    }
}
