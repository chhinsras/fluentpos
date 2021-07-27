using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands.Validators;
using Microsoft.Extensions.Localization;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.ExtendedAttributes.Validators.Categories
{
    public class RemoveCategoryExtendedAttributeCommandValidator : RemoveExtendedAttributeCommandValidator<Guid, Category>
    {
        public RemoveCategoryExtendedAttributeCommandValidator(IStringLocalizer<RemoveCategoryExtendedAttributeCommandValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}