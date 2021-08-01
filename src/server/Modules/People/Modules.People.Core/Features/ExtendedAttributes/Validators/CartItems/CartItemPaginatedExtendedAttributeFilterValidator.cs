using System;
using FluentPOS.Modules.People.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.People.Core.Features.ExtendedAttributes.Validators.CartItems
{
    public class CartItemPaginatedExtendedAttributeFilterValidator : PaginatedExtendedAttributeFilterValidator<Guid, CartItem>
    {
        public CartItemPaginatedExtendedAttributeFilterValidator(IStringLocalizer<CartItemPaginatedExtendedAttributeFilterValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}