// --------------------------------------------------------------------------------------------------
// <copyright file="GetExtendedAttributesQuery.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

#nullable enable

using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Filters;
using FluentPOS.Shared.Core.Mappings.Converters;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.ExtendedAttributes;
using MediatR;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries
{
    public class GetExtendedAttributesQuery<TEntityId, TEntity> : IRequest<PaginatedResult<GetExtendedAttributesResponse<TEntityId>>>
        where TEntity : class, IEntity<TEntityId>
    {
        public int PageNumber { get; }

        public int PageSize { get; }

        public string? SearchString { get; }

        public string[] OrderBy { get; }

        public TEntityId? EntityId { get; }

        public ExtendedAttributeType? Type { get; }

        public GetExtendedAttributesQuery(PaginatedExtendedAttributeFilter<TEntityId, TEntity> filter)
        {
            PageNumber = filter.PageNumber;
            PageSize = filter.PageSize;
            SearchString = filter.SearchString;
            OrderBy = new OrderByConverter().Convert(filter.OrderBy);
            EntityId = filter.EntityId;
            Type = filter.Type;
        }
    }
}