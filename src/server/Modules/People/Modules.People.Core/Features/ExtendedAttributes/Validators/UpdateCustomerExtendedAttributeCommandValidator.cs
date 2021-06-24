using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.ExtendedAttributes.Validators
{
    public class UpdateCustomerExtendedAttributeCommandValidator : UpdateExtendedAttributeCommandValidator<Guid, Customer>
    {
        public UpdateCustomerExtendedAttributeCommandValidator(IStringLocalizer<UpdateCustomerExtendedAttributeCommandValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}