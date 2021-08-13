// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedCartItemFilterValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Features.Common.Queries.Validators;
using FluentPOS.Shared.DTOs.People.CartItems;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.CartItems.Queries.Validators
{
    public class PaginatedCartItemFilterValidator : PaginatedFilterValidator<Guid, CartItem, PaginatedCartItemFilter>
    {
        public PaginatedCartItemFilterValidator(IStringLocalizer<PaginatedCartItemFilterValidator> localizer)
            : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}