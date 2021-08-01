using System;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.ExtendedAttributes.Validators.Products
{
    public class PaginatedProductExtendedAttributeFilterValidator : PaginatedExtendedAttributeFilterValidator<Guid, Product>
    {
        public PaginatedProductExtendedAttributeFilterValidator(IStringLocalizer<PaginatedProductExtendedAttributeFilterValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}