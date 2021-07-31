using System;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Features.Common.Queries.Validators;
using FluentPOS.Shared.DTOs.Catalogs.Products;
using Microsoft.Extensions.Localization;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Queries.Validators
{
    public class ProductPaginatedFilterValidator : PaginatedFilterValidator<Guid, Product, PaginatedProductFilter>
    {
        public ProductPaginatedFilterValidator(IStringLocalizer<ProductPaginatedFilterValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}