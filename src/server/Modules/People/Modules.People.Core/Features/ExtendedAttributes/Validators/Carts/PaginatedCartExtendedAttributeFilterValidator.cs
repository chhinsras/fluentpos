using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.ExtendedAttributes.Validators.Carts
{
    public class PaginatedCartExtendedAttributeFilterValidator : PaginatedExtendedAttributeFilterValidator<Guid, Cart>
    {
        public PaginatedCartExtendedAttributeFilterValidator(IStringLocalizer<PaginatedCartExtendedAttributeFilterValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}