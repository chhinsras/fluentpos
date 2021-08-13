// --------------------------------------------------------------------------------------------------
// <copyright file="GetCartByIdResponse.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using FluentPOS.Shared.DTOs.People.CartItems;
using FluentPOS.Shared.DTOs.People.Customers;

namespace FluentPOS.Shared.DTOs.People.Carts
{
    public record GetCartByIdResponse(Guid Id, Guid CustomerId, DateTime Timestamp)
    {
        public GetCustomerByIdResponse Customer { get; set; }
        public ICollection<GetCartItemByIdResponse> CartItems { get; set; }
    }
}