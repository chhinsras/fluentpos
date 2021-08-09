// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedProductFilterValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.Common.Queries.Validators;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Queries.Validators
{
    public class PaginatedProductFilterValidator : PaginatedFilterValidator<Guid, Product, PaginatedProductFilter>
    {
        public PaginatedProductFilterValidator(IStringLocalizer<PaginatedProductFilterValidator> localizer)
            : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}