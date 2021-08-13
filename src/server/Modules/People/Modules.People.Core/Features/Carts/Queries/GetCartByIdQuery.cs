// --------------------------------------------------------------------------------------------------
// <copyright file="GetCartByIdQuery.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Constants;
using FluentPOS.Shared.Core.Queries;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.People.Carts;
using MediatR;

namespace FluentPOS.Modules.People.Core.Features.Carts.Queries
{
    public class GetCartByIdQuery : IRequest<Result<GetCartByIdResponse>>, ICacheable
    {
        public Guid Id { get; protected set; }

        public bool BypassCache { get; protected set; }

        public string CacheKey { get; protected set; }

        public TimeSpan? SlidingExpiration { get; protected set; }

        public GetCartByIdQuery(Guid cartId, bool bypassCache = false, TimeSpan? slidingExpiration = null)
        {
            Id = cartId;
            BypassCache = bypassCache;
            CacheKey = CacheKeys.Common.GetEntityByIdCacheKey<Guid, Cart>(cartId);
            SlidingExpiration = slidingExpiration;
        }

        public GetCartByIdQuery()
        {
        }
    }
}