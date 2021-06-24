#nullable enable
using System;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.DTOs.ExtendedAttributes;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Events
{
    public class ExtendedAttributeUpdatedEvent<TEntityId, TEntity> : Event
    {
        public ExtendedAttributeUpdatedEvent(Guid id, TEntityId entityId, ExtendedAttributeType type,
            string key, decimal? @decimal, string? text, DateTime? dateTime, string? json, bool? boolean, int? integer, string? externalId,
            string? group, string? description, bool isActive)
        {
            EntityId = entityId;
            EntityName = typeof(TEntity).Name;
            Type = type;
            Key = key;
            Decimal = @decimal;
            Text = text;
            DateTime = dateTime;
            Json = json;
            Boolean = boolean;
            Integer = integer;
            ExternalId = externalId;
            Group = @group;
            Description = description;
            IsActive = isActive;
            AggregateId = id;
        }

        public TEntityId EntityId { get; set; }
        public string EntityName { get; set; }
        public ExtendedAttributeType Type { get; set; }
        public string Key { get; set; }
        public decimal? Decimal { get; set; }
        public string? Text { get; set; }
        public DateTime? DateTime { get; set; }
        public string? Json { get; set; }
        public bool? Boolean { get; set; }
        public int? Integer { get; set; }
        public string? ExternalId { get; set; }
        public string? Group { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }
    }
}