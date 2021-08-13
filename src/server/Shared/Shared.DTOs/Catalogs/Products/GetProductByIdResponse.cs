// --------------------------------------------------------------------------------------------------
// <copyright file="GetProductByIdResponse.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;

namespace FluentPOS.Shared.DTOs.Catalogs.Products
{
    public record GetProductByIdResponse(Guid Id, string Name, string LocaleName, string BarcodeSymbology, string Detail, Guid BrandId, Guid CategoryId, decimal Price, decimal Cost, string ImageUrl, decimal Tax, string TaxMethod, bool IsAlert, decimal AlertQuantity);
}