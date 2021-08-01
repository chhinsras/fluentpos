using System;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.Common.Queries.Validators;
using FluentPOS.Shared.DTOs.Catalogs.Categories;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.Categories.Queries.Validators
{
    public class PaginatedCategoryFilterValidator : PaginatedFilterValidator<Guid, Category, PaginatedCategoryFilter>
    {
        public PaginatedCategoryFilterValidator(IStringLocalizer<PaginatedCategoryFilterValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}