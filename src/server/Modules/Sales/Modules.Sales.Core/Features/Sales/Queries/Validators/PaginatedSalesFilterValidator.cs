// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedSaleFilterValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.Sales.Core.Entities;
using FluentPOS.Shared.Core.Features.Common.Queries.Validators;
using FluentPOS.Shared.DTOs.Catalogs.Brands;
using FluentPOS.Shared.DTOs.Sales.Orders;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Queries.Validators
{
    public class PaginatedSalesFilterValidator : PaginatedFilterValidator<Guid, Order, PaginatedSalesFilter>
    {
        public PaginatedSalesFilterValidator(IStringLocalizer<PaginatedSalesFilterValidator> localizer)
            : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}