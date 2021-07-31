using System;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.ExtendedAttributes.Validators.Brands
{
    public class BrandPaginatedExtendedAttributeFilterValidator : PaginatedExtendedAttributeFilterValidator<Guid, Brand>
    {
        public BrandPaginatedExtendedAttributeFilterValidator(IStringLocalizer<BrandPaginatedExtendedAttributeFilterValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}