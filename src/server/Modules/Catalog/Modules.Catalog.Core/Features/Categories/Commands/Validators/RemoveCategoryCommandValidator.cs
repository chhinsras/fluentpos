using FluentValidation;
using Microsoft.Extensions.Localization;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Commands.Validators
{
    public class RemoveCategoryCommandValidator : AbstractValidator<RemoveCategoryCommand>
    {
        public RemoveCategoryCommandValidator(IStringLocalizer<RemoveCategoryCommandValidator> localizer)
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
        }
    }
}