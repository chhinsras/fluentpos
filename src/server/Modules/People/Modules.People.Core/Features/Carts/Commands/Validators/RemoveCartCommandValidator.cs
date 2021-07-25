using System;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Carts.Commands.Validators
{
    public class RemoveCartCommandValidator : AbstractValidator<RemoveCartCommand>
    {
        public RemoveCartCommandValidator(IStringLocalizer<RemoveCartCommandValidator> localizer)
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty).WithMessage(x => localizer["The {PropertyName} property cannot be empty."]);
        }
    }
}