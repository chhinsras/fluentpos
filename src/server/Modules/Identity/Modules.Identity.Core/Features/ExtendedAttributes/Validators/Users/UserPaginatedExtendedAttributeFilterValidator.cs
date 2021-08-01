using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Identity.Core.Features.ExtendedAttributes.Validators.Users
{
    public class UserPaginatedExtendedAttributeFilterValidator : PaginatedExtendedAttributeFilterValidator<string, FluentUser>
    {
        public UserPaginatedExtendedAttributeFilterValidator(IStringLocalizer<UserPaginatedExtendedAttributeFilterValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}