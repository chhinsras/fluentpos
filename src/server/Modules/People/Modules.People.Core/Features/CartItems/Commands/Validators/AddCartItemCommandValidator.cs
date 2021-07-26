using FluentValidation;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.CartItems.Commands.Validators
{
    public class AddCartItemCommandValidator : AbstractValidator<AddCartItemCommand>
    {
        public AddCartItemCommandValidator(IStringLocalizer<AddCartItemCommandValidator> localizer)
        {
            RuleFor(c => c.CartId)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);
            RuleFor(c => c.ProductId)
                .NotEmpty().WithMessage(localizer["The {PropertyName} property cannot be empty."]);
            RuleFor(c => c.Quantity)
                .GreaterThan(0).WithMessage(localizer["The {PropertyName} property should be greater than 0."]);
        }
    }
}