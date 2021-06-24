using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Identity.Core.Features.ExtendedAttributes.Validators
{
    public class AddUserExtendedAttributeCommandValidator : AddExtendedAttributeCommandValidator<string, FluentUser>
    {
        public AddUserExtendedAttributeCommandValidator(IStringLocalizer<AddUserExtendedAttributeCommandValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}