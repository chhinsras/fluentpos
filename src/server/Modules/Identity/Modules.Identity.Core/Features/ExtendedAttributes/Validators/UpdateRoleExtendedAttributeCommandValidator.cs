using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Commands.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Identity.Core.Features.ExtendedAttributes.Validators
{
    public class UpdateRoleExtendedAttributeCommandValidator : UpdateExtendedAttributeCommandValidator<string, FluentRole>
    {
        public UpdateRoleExtendedAttributeCommandValidator(IStringLocalizer<UpdateRoleExtendedAttributeCommandValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}