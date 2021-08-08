// <copyright file="PaginatedCartItemFilter.cs" company="Fluentpos">
// --------------------------------------------------------------------------------------------------
// Copyright (c) Fluentpos. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// --------------------------------------------------------------------------------------------------
// </copyright>

namespace FluentPOS.Shared.DTOs.People.CartItems
{
    using System;
    using FluentPOS.Shared.DTOs.Filters;
    public class PaginatedCartItemFilter : PaginatedFilter
    {
        public string SearchString { get; set; }

        public Guid? CartId { get; set; }

        public Guid? ProductId { get; set; }
    }
}