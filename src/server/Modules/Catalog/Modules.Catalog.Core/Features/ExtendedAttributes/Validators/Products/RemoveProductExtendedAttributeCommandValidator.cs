// --------------------------------------------------------------------------------------------------
// <copyright file="RemoveProductExtendedAttributeCommandValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.ExtendedAttributes.Validators.Products
{
    public class RemoveProductExtendedAttributeCommandValidator : RemoveExtendedAttributeCommandValidator<Guid, Product>
    {
        public RemoveProductExtendedAttributeCommandValidator(IStringLocalizer<RemoveProductExtendedAttributeCommandValidator> localizer)
            : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}