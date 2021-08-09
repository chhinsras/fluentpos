// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedCartItemFilter.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.DTOs.Filters;

namespace FluentPOS.Shared.DTOs.People.CartItems
{
    public class PaginatedCartItemFilter : PaginatedFilter
    {
        public string SearchString { get; set; }

        public Guid? CartId { get; set; }

        public Guid? ProductId { get; set; }
    }
}