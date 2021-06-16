using FluentValidation;
using System;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Commands.Validators
{
    public class RemoveBrandCommandValidator : AbstractValidator<RemoveBrandCommand>
    {
        public RemoveBrandCommandValidator(IStringLocalizer<RemoveBrandCommandValidator> localizer)
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}