using System;

namespace FluentPOS.Modules.People.Core.Constants
{
    public static class PeopleCacheKeys
    {
        public static string GetCustomerByIdCacheKey(Guid id)
        {
            return $"Customer-{id}";
        }
    }
}