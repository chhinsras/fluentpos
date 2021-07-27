using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Identity.Core.Features.ExtendedAttributes.Validators.Users
{
    public class RemoveUserExtendedAttributeCommandValidator : RemoveExtendedAttributeCommandValidator<string, FluentUser>
    {
        public RemoveUserExtendedAttributeCommandValidator(IStringLocalizer<RemoveUserExtendedAttributeCommandValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}