#nullable enable

using FluentPOS.Shared.DTOs.ExtendedAttributes;
using System;

namespace FluentPOS.Shared.Core.Contracts
{
    public interface IExtendedAttribute<TEntityId>
    {
        public TEntityId EntityId { get; set; }

        public ExtendedAttributeType Type { get; set; }

        public string Key { get; set; }

        public decimal? Decimal { get; set; }

        public string? Text { get; set; }

        public DateTime? DateTime { get; set; }

        public string? Json { get; set; }

        public string? ExternalId { get; set; }

        public string? Group { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }
}