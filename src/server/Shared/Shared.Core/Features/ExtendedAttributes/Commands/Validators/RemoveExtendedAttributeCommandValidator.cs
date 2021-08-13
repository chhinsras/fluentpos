// --------------------------------------------------------------------------------------------------
// <copyright file="RemoveExtendedAttributeCommandValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Shared.Core.Contracts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands.Validators
{
    public abstract class RemoveExtendedAttributeCommandValidator<TEntityId, TEntity> : AbstractValidator<RemoveExtendedAttributeCommand<TEntityId, TEntity>>
        where TEntity : class, IEntity<TEntityId>
    {
        protected RemoveExtendedAttributeCommandValidator(IStringLocalizer localizer)
        {
            RuleFor(request => request.Id)
                .NotEqual(Guid.Empty).WithMessage(_ => localizer["The {PropertyName} property cannot be empty."]);
        }
    }
}