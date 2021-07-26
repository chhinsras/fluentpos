using System;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Carts.Commands.Validators
{
    public class CreateCartCommandValidator : AbstractValidator<CreateCartCommand>
    {
        public CreateCartCommandValidator(IStringLocalizer<CreateCartCommandValidator> localizer)
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty).WithMessage(x => localizer["The {PropertyName} property cannot be empty."]);
        }
    }
}