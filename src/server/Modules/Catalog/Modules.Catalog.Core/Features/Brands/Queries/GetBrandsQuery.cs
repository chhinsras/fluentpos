// --------------------------------------------------------------------------------------------------
// <copyright file="GetBrandsQuery.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.Core.Wrapper;
using FluentPOS.Shared.DTOs.Catalogs.Brands;
using MediatR;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Queries
{
    public class GetBrandsQuery : IRequest<PaginatedResult<GetBrandsResponse>>
    {
        public int PageNumber { get; private set; }

        public int PageSize { get; private set; }

        public string[] OrderBy { get; private set; }

        public string SearchString { get; private set; }
    }
}