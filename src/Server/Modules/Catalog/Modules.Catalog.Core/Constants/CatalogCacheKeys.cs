using System;

namespace FluentPOS.Modules.Catalog.Core.Constants
{
    public static class CatalogCacheKeys
    {
        public static string GetBrandByIdCacheKey(Guid id)
        {
            return $"Brand-{id}";
        }

        public static string GetCategoryByIdCacheKey(Guid id)
        {
            return $"Category-{id}";
        }

        public static string GetProductByIdCacheKey(Guid id)
        {
            return $"Product-{id}";
        }
    }
}