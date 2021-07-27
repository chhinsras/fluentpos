using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands.Validators;
using Microsoft.Extensions.Localization;
using System;

namespace FluentPOS.Modules.People.Core.Features.ExtendedAttributes.Validators.Customers
{
    public class RemoveCustomerExtendedAttributeCommandValidator : RemoveExtendedAttributeCommandValidator<Guid, Customer>
    {
        public RemoveCustomerExtendedAttributeCommandValidator(IStringLocalizer<RemoveCustomerExtendedAttributeCommandValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}