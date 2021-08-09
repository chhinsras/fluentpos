// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedCategoryFilterValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.Common.Queries.Validators;
using FluentPOS.Shared.DTOs.Catalogs.Categories;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Queries.Validators
{
    public class PaginatedCategoryFilterValidator : PaginatedFilterValidator<Guid, Category, PaginatedCategoryFilter>
    {
        public PaginatedCategoryFilterValidator(IStringLocalizer<PaginatedCategoryFilterValidator> localizer)
            : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}