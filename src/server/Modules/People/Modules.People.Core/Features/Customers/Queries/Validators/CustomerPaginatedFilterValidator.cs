using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Features.Common.Queries.Validators;
using FluentPOS.Shared.DTOs.People.Customers;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries.Validators
{
    public class CustomerPaginatedFilterValidator : PaginatedFilterValidator<Guid, Customer, PaginatedCustomerFilter>
    {
        public CustomerPaginatedFilterValidator(IStringLocalizer<CustomerPaginatedFilterValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}