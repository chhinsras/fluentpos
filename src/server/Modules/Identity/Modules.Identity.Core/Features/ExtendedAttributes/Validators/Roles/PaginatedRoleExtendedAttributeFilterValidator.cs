using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Identity.Core.Features.ExtendedAttributes.Validators.Roles
{
    public class PaginatedRoleExtendedAttributeFilterValidator : PaginatedExtendedAttributeFilterValidator<string, FluentRole>
    {
        public PaginatedRoleExtendedAttributeFilterValidator(IStringLocalizer<PaginatedRoleExtendedAttributeFilterValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}