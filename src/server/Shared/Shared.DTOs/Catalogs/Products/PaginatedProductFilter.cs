// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedProductFilter.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.DTOs.Filters;

namespace FluentPOS.Shared.DTOs.Catalogs.Products
{
    public class PaginatedProductFilter : PaginatedFilter
    {
        public string SearchString { get; set; }

        public Guid[] BrandIds { get; set; }

        public Guid[] CategoryIds { get; set; }
    }
}