using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands.Validators;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using Microsoft.Extensions.Localization;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.ExtendedAttributes.Validators.Categories
{
    public class AddCategoryExtendedAttributeCommandValidator : AddExtendedAttributeCommandValidator<Guid, Category>
    {
        public AddCategoryExtendedAttributeCommandValidator(IStringLocalizer<AddCategoryExtendedAttributeCommandValidator> localizer, IJsonSerializer jsonSerializer) : base(localizer, jsonSerializer)
        {
            // you can override the validation rules here
        }
    }
}