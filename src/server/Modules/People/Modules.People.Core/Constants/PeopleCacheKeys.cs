using System;

namespace FluentPOS.Modules.People.Core.Constants
{
    public static class PeopleCacheKeys
    {
        public static string GetCustomerByIdCacheKey(Guid id)
        {
            return $"Customer-{id}";
        }

        public static string GetCartByIdCacheKey(Guid id)
        {
            return $"Cart-{id}";
        }

        public static string GetCartItemByIdCacheKey(Guid id)
        {
            return $"CartItem-{id}";
        }
    }
}