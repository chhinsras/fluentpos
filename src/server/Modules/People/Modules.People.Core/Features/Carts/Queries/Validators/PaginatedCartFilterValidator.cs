// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedCartFilterValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Features.Common.Queries.Validators;
using FluentPOS.Shared.DTOs.People.Carts;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Carts.Queries.Validators
{
    public class PaginatedCartFilterValidator : PaginatedFilterValidator<Guid, Cart, PaginatedCartFilter>
    {
        public PaginatedCartFilterValidator(IStringLocalizer<PaginatedCartFilterValidator> localizer)
            : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}