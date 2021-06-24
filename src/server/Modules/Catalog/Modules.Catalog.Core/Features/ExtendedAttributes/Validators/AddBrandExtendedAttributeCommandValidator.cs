using System;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.ExtendedAttributes.Validators
{
    public class AddBrandExtendedAttributeCommandValidator : AddExtendedAttributeCommandValidator<Guid, Brand>
    {
        public AddBrandExtendedAttributeCommandValidator(IStringLocalizer<AddBrandExtendedAttributeCommandValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}