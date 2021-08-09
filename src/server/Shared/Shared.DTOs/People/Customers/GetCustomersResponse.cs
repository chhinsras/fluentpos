// --------------------------------------------------------------------------------------------------
// <copyright file="GetCustomersResponse.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace FluentPOS.Shared.DTOs.People.Customers
{
    public record GetCustomersResponse(Guid Id, string Name, string Phone, string Email, string ImageUrl, string Type);
}