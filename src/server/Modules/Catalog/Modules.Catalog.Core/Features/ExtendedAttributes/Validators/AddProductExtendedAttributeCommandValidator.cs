using System;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.ExtendedAttributes.Validators
{
    public class AddProductExtendedAttributeCommandValidator : AddExtendedAttributeCommandValidator<Guid, Product>
    {
        public AddProductExtendedAttributeCommandValidator(IStringLocalizer<AddProductExtendedAttributeCommandValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}