using FluentPOS.Shared.Core.Contracts;

namespace FluentPOS.Shared.Core.Constants
{
    public static class CacheKeys
    {
        public static class Common
        {
            public static string GetEntityByIdCacheKey<TEntityId, TEntity>(TEntityId id) where TEntity : class, IEntity<TEntityId>
            {
                return $"GetEntity-{typeof(TEntity).FullName}-{id}";
            }
        }
    }
}