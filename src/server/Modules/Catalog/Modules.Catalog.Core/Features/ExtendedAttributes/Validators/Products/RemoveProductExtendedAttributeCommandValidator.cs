using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands.Validators;
using Microsoft.Extensions.Localization;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.ExtendedAttributes.Validators.Products
{
    public class RemoveProductExtendedAttributeCommandValidator : RemoveExtendedAttributeCommandValidator<Guid, Product>
    {
        public RemoveProductExtendedAttributeCommandValidator(IStringLocalizer<RemoveProductExtendedAttributeCommandValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}