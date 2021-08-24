// --------------------------------------------------------------------------------------------------
// <copyright file="ExtendedAttributeAddedEvent.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

#nullable enable

using System;
using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Domain;
using FluentPOS.Shared.Core.Utilities;
using FluentPOS.Shared.DTOs.ExtendedAttributes;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Events
{
    public class ExtendedAttributeAddedEvent<TEntityId, TEntity> : Event
        where TEntity : class, IEntity<TEntityId>
    {
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

        public ExtendedAttributeAddedEvent(ExtendedAttribute<TEntityId, TEntity> extendedAttribute)
        {
            EntityId = extendedAttribute.EntityId;
            EntityName = typeof(TEntity).GetGenericTypeName();
            Type = extendedAttribute.Type;
            Key = extendedAttribute.Key;
            Decimal = extendedAttribute.Decimal;
            Text = extendedAttribute.Text;
            DateTime = extendedAttribute.DateTime;
            Json = extendedAttribute.Json;
            Boolean = extendedAttribute.Boolean;
            Integer = extendedAttribute.Integer;
            ExternalId = extendedAttribute.ExternalId;
            Group = extendedAttribute.Group;
            Description = extendedAttribute.Description;
            IsActive = extendedAttribute.IsActive;
            AggregateId = extendedAttribute.Id;
            RelatedEntities = new[] { typeof(TEntity) };
        }
    }
}