// --------------------------------------------------------------------------------------------------
// <copyright file="GetCategoryByIdQuery.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.Core.Queries;
using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Categories;
using MediatR;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Queries
{
    public class GetCategoryByIdQuery : IRequest<Result<GetCategoryByIdResponse>>, ICacheable
    {
        public Guid Id { get; protected set; }

        public bool BypassCache { get; protected set; }

        public string CacheKey { get; protected set; }

        public TimeSpan? SlidingExpiration { get; protected set; }
    }
}