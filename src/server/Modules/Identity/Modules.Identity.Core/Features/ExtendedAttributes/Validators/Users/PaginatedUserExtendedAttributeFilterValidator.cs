using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Identity.Core.Features.ExtendedAttributes.Validators.Users
{
    public class PaginatedUserExtendedAttributeFilterValidator : PaginatedExtendedAttributeFilterValidator<string, FluentUser>
    {
        public PaginatedUserExtendedAttributeFilterValidator(IStringLocalizer<PaginatedUserExtendedAttributeFilterValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}