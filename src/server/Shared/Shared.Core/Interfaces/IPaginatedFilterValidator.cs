// --------------------------------------------------------------------------------------------------
// <copyright file="IPaginatedFilterValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Extensions;
using FluentPOS.Shared.DTOs.Filters;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Shared.Core.Interfaces
{
    internal interface IPaginatedFilterValidator<TEntityId, TEntity, TFilter>
        where TEntity : class, IEntity<TEntityId>
        where TFilter : PaginatedFilter
    {
        static void UseRules(AbstractValidator<TFilter> validator, IStringLocalizer localizer)
        {
            validator.RuleFor(request => request.PageNumber)
                .GreaterThan(0).WithMessage(localizer["The {PropertyName} property must be greater than 0."]);
            validator.RuleFor(request => request.PageSize)
                .GreaterThan(0).WithMessage(localizer["The {PropertyName} property must be greater than 0."]);
            validator.RuleFor(request => request.OrderBy)
                .MustContainCorrectOrderingsFor(typeof(TEntity), localizer);
        }
    }
}