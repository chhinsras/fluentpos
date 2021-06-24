using System;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.ExtendedAttributes.Validators
{
    public class AddCategoryExtendedAttributeCommandValidator : AddExtendedAttributeCommandValidator<Guid, Category>
    {
        public AddCategoryExtendedAttributeCommandValidator(IStringLocalizer<AddCategoryExtendedAttributeCommandValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}