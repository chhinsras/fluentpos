using System;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands.Validators;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.ExtendedAttributes.Validators
{
    public class UpdateCategoryExtendedAttributeCommandValidator : UpdateExtendedAttributeCommandValidator<Guid, Category>
    {
        public UpdateCategoryExtendedAttributeCommandValidator(IStringLocalizer<UpdateCategoryExtendedAttributeCommandValidator> localizer, IJsonSerializer jsonSerializer) : base(localizer, jsonSerializer)
        {
            // you can override the validation rules here
        }
    }
}