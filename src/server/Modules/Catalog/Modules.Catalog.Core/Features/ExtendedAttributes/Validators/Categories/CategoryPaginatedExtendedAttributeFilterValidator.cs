using System;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.ExtendedAttributes.Queries.Validators;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.ExtendedAttributes.Validators.Categories
{
    public class CategoryPaginatedExtendedAttributeFilterValidator : PaginatedExtendedAttributeFilterValidator<Guid, Category>
    {
        public CategoryPaginatedExtendedAttributeFilterValidator(IStringLocalizer<CategoryPaginatedExtendedAttributeFilterValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}