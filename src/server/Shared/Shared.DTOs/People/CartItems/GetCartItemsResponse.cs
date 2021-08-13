// --------------------------------------------------------------------------------------------------
// <copyright file="GetCartItemsResponse.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace FluentPOS.Shared.DTOs.People.CartItems
{
    public record GetCartItemsResponse(Guid Id, Guid CartId, Guid ProductId, int Quantity)
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Rate { get; set; }
    }
}