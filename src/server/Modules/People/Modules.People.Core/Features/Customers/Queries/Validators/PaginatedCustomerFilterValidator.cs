using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Features.Common.Queries.Validators;
using FluentPOS.Shared.DTOs.People.Customers;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Customers.Queries.Validators
{
    public class PaginatedCustomerFilterValidator : PaginatedFilterValidator<Guid, Customer, PaginatedCustomerFilter>
    {
        public PaginatedCustomerFilterValidator(IStringLocalizer<PaginatedCustomerFilterValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}