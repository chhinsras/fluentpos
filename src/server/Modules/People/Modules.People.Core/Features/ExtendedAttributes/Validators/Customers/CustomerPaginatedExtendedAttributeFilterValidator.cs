using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.ExtendedAttributes.Validators.Customers
{
    public class CustomerPaginatedExtendedAttributeFilterValidator : PaginatedExtendedAttributeFilterValidator<Guid, Customer>
    {
        public CustomerPaginatedExtendedAttributeFilterValidator(IStringLocalizer<CustomerPaginatedExtendedAttributeFilterValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}