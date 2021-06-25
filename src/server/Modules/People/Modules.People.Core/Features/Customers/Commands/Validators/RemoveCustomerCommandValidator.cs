using FluentValidation;
using Microsoft.Extensions.Localization;
using System;

namespace FluentPOS.Modules.People.Core.Features.Customers.Commands.Validators
{
    public class RemoveCustomerCommandValidator : AbstractValidator<RemoveCustomerCommand>
    {
        public RemoveCustomerCommandValidator(IStringLocalizer<RemoveCustomerCommandValidator> localizer)
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty).WithMessage(x => localizer["The {PropertyName} property cannot be empty."]);
        }
    }
}