using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Features.Common.Queries.Validators;
using FluentPOS.Shared.DTOs.People.Carts;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.Carts.Queries.Validators
{
    public class CartPaginatedFilterValidator : PaginatedFilterValidator<Guid, Cart, PaginatedCartFilter>
    {
        public CartPaginatedFilterValidator(IStringLocalizer<CartPaginatedFilterValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}