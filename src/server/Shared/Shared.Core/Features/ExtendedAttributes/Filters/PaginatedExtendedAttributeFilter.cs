// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedExtendedAttributeFilter.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

#nullable enable

using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.DTOs.ExtendedAttributes;
using FluentPOS.Shared.DTOs.Filters;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Filters
{
    // ReSharper disable once UnusedTypeParameter
    public class PaginatedExtendedAttributeFilter<TEntityId, TEntity> : PaginatedFilter
        where TEntity : class, IEntity<TEntityId>
    {
        public string? SearchString { get; set; }

        public TEntityId? EntityId { get; set; }

        public ExtendedAttributeType? Type { get; set; }
    }
}