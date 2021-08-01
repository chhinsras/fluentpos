using System;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.Common.Queries.Validators;
using FluentPOS.Shared.DTOs.Catalogs.Brands;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.Brands.Queries.Validators
{
    public class PaginatedBrandFilterValidator : PaginatedFilterValidator<Guid, Brand, PaginatedBrandFilter>
    {
        public PaginatedBrandFilterValidator(IStringLocalizer<PaginatedBrandFilterValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}