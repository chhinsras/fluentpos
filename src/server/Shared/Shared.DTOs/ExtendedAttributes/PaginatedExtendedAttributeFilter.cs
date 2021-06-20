#nullable enable
using System;
using FluentPOS.Shared.DTOs.Filters;

namespace FluentPOS.Shared.DTOs.ExtendedAttributes
{
    public class PaginatedExtendedAttributeFilter : PaginatedFilter
    {
        public string? SearchString { get; set; }
        public Guid? EntityId { get; set; }
        public ExtendedAttributeType? Type { get; set; }
    }
}