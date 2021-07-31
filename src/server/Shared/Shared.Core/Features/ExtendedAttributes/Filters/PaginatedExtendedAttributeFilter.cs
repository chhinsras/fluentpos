#nullable enable

using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.DTOs.ExtendedAttributes;
using FluentPOS.Shared.DTOs.Filters;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Filters
{
    public class PaginatedExtendedAttributeFilter<TEntityId, TEntity> : PaginatedFilter
        where TEntity : class, IEntity<TEntityId>
    {
        public string? SearchString { get; set; }
        public TEntityId? EntityId { get; set; }
        public ExtendedAttributeType? Type { get; set; }
    }
}