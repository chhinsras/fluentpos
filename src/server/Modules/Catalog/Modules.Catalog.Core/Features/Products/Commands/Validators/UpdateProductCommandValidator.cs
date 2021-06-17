using FluentValidation;
using Microsoft.Extensions.Localization;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Commands.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator(IStringLocalizer<UpdateProductCommandValidator> localizer)
        {
            RuleFor(c => c.Id)
               .NotEqual(Guid.Empty);
            RuleFor(c => c.Name)
               .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."])
               .Length(2, 150).WithMessage(localizer["The {PropertyName} property must have between 2 and 150 characters."]);
            RuleFor(c => c.Detail)
               .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."])
               .Length(2, 150).WithMessage(localizer["The {PropertyName} property must have between 2 and 150 characters."]);
            RuleFor(c => c.BrandId)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);
            RuleFor(c => c.CategoryId)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);
            RuleFor(c => c.Price)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);
            RuleFor(c => c.Cost)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);
            RuleFor(c => c.BarcodeSymbology)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."])
                .Length(5, 150).WithMessage(localizer["The {PropertyName} property must have between 5 and 150 characters."]);
        }
    }
}