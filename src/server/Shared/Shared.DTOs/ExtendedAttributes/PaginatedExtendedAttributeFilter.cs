#nullable enable
using FluentPOS.Shared.DTOs.Filters;

namespace FluentPOS.Shared.DTOs.ExtendedAttributes
{
    public class PaginatedExtendedAttributeFilter<TEntityId> : PaginatedFilter
    {
        public string? SearchString { get; set; }
        public TEntityId? EntityId { get; set; }
        public ExtendedAttributeType? Type { get; set; }
    }
}