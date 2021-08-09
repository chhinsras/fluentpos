// --------------------------------------------------------------------------------------------------
// <copyright file="GetExtendedAttributeByIdResponse.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

#nullable enable

using System;

namespace FluentPOS.Shared.DTOs.ExtendedAttributes
{
    public record GetExtendedAttributeByIdResponse<TEntityId>(
        Guid Id,
        TEntityId EntityId,
        ExtendedAttributeType Type,
        string Key,
        decimal? Decimal,
        string? Text,
        DateTime? DateTime,
        string? Json,
        bool? Boolean,
        int? Integer,
        string? ExternalId,
        string? Group,
        string? Description,
        bool IsActive);
}