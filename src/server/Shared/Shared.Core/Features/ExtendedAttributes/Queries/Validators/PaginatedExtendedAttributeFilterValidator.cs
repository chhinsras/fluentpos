// --------------------------------------------------------------------------------------------------
// <copyright file="PaginatedExtendedAttributeFilterValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Shared.Core.Contracts;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Filters;
using FluentPOS.Shared.Core.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries.Validators
{
    public abstract class PaginatedExtendedAttributeFilterValidator<TEntityId, TEntity> :
        AbstractValidator<PaginatedExtendedAttributeFilter<TEntityId, TEntity>>,
        IPaginatedFilterValidator<TEntityId, TEntity, PaginatedExtendedAttributeFilter<TEntityId, TEntity>>
            where TEntity : class, IEntity<TEntityId>
    {
        protected PaginatedExtendedAttributeFilterValidator(IStringLocalizer localizer)
        {
            IPaginatedFilterValidator<TEntityId, TEntity, PaginatedExtendedAttributeFilter<TEntityId, TEntity>>.UseRules(this, localizer);
        }
    }
}