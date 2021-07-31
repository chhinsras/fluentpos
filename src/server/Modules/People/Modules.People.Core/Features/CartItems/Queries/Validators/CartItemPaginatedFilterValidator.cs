using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Features.Common.Queries.Validators;
using FluentPOS.Shared.DTOs.People.CartItems;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.CartItems.Queries.Validators
{
    public class CartItemPaginatedFilterValidator : PaginatedFilterValidator<Guid, CartItem, PaginatedCartItemFilter>
    {
        public CartItemPaginatedFilterValidator(IStringLocalizer<CartItemPaginatedFilterValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}