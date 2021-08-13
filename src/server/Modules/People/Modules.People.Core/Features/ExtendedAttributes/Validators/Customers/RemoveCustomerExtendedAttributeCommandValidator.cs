// --------------------------------------------------------------------------------------------------
// <copyright file="RemoveCustomerExtendedAttributeCommandValidator.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.ExtendedAttributes.Validators.Customers
{
    public class RemoveCustomerExtendedAttributeCommandValidator : RemoveExtendedAttributeCommandValidator<Guid, Customer>
    {
        public RemoveCustomerExtendedAttributeCommandValidator(IStringLocalizer<RemoveCustomerExtendedAttributeCommandValidator> localizer)
            : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}