using System;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.ExtendedAttributes.Validators.Products
{
    public class ProductPaginatedExtendedAttributeFilterValidator : PaginatedExtendedAttributeFilterValidator<Guid, Product>
    {
        public ProductPaginatedExtendedAttributeFilterValidator(IStringLocalizer<ProductPaginatedExtendedAttributeFilterValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}