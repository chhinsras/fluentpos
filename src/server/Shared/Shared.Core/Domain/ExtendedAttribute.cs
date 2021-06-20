#nullable enable
using System;
using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.DTOs.ExtendedAttributes;

namespace FluentPOS.Shared.Core.Domain
{
    public abstract class ExtendedAttribute<TEntity>
        : BaseEntity, IExtendedAttribute
        where TEntity : BaseEntity
    {
        public Guid EntityId { get; set; }

        public virtual TEntity Entity { get; set; }

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