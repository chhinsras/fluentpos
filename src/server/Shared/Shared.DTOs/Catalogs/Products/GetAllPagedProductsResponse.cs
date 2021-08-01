using System;

namespace FluentPOS.Shared.DTOs.Catalogs.Products
{
    public record GetAllPagedProductsResponse(Guid Id, string Name, string LocaleName, string BarcodeSymbology, string Detail, string BrandName, string CategoryName, decimal Price, decimal Cost, string ImageUrl, string Tax, string TaxMethod, bool IsAlert, decimal AlertQuantity);
}