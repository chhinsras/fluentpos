using FluentValidation;
using Microsoft.Extensions.Localization;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Commands.Validators
{
    public class UpdateBrandCommandValidator : AbstractValidator<UpdateBrandCommand>
    {
        public UpdateBrandCommandValidator(IStringLocalizer<UpdateBrandCommandValidator> localizer)
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty);
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."])
                .Length(2, 150).WithMessage(localizer["The {PropertyName} property must have between 2 and 150 characters."]);
            RuleFor(c => c.Detail)
               .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."])
               .Length(2, 150).WithMessage(localizer["The {PropertyName} property must have between 2 and 150 characters."]);
        }
    }
}