// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedUserExtendedAttributeFilterValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Identity.Core.Features.ExtendedAttributes.Validators.Users
{
    public class PaginatedUserExtendedAttributeFilterValidator : PaginatedExtendedAttributeFilterValidator<string, FluentUser>
    {
        public PaginatedUserExtendedAttributeFilterValidator(IStringLocalizer<PaginatedUserExtendedAttributeFilterValidator> localizer)
            : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}