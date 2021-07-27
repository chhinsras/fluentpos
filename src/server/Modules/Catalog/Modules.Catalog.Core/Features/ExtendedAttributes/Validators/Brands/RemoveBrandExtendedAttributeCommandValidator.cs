using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands.Validators;
using Microsoft.Extensions.Localization;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.ExtendedAttributes.Validators.Brands
{
    public class RemoveBrandExtendedAttributeCommandValidator : RemoveExtendedAttributeCommandValidator<Guid, Brand>
    {
        public RemoveBrandExtendedAttributeCommandValidator(IStringLocalizer<RemoveBrandExtendedAttributeCommandValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}