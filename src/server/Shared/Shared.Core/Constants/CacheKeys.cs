using System;

namespace FluentPOS.Shared.Core.Constants
{
    public static class CacheKeys
    {
        public static string GetExtendedAttributeByIdCacheKey(string entityName, Guid id)
        {
            return $"{entityName}-{id}";
        }
    }
}