// --------------------------------------------------------------------------------------------------
// <copyright file="RemoveCartCommandValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Carts.Commands.Validators
{
    public class RemoveCartCommandValidator : AbstractValidator<RemoveCartCommand>
    {
        public RemoveCartCommandValidator(IStringLocalizer<RemoveCartCommandValidator> localizer)
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty).WithMessage(x => localizer["The {PropertyName} property cannot be empty."]);
        }
    }
}