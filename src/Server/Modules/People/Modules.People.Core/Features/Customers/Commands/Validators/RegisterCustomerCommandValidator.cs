using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Core.Features.Customers.Commands.Validators
{
    public class RegisterCustomerCommandValidator : AbstractValidator<RegisterCustomerCommand>
    {
        public RegisterCustomerCommandValidator(IStringLocalizer<RegisterCustomerCommandValidator> localizer)
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."])
                .Length(2, 150).WithMessage(localizer["The {PropertyName} property must have between 2 and 150 characters."]);
            RuleFor(c => c.Phone)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."])
                .Length(2, 30).WithMessage(localizer["The {PropertyName} property must have between 2 and 30 characters."]);
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);
            RuleFor(c => c.Type)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);
        }
    }
}
