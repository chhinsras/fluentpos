using System;

namespace FluentPOS.Shared.DTOs.Catalogs.Products
{
    public record GetProductsResponse(Guid Id, string Name, string LocaleName, string BarcodeSymbology, string Detail, Guid BrandId, string BrandName, Guid CategoryId, string CategoryName, decimal Price, decimal Cost, string ImageUrl, string Tax, string TaxMethod, bool IsAlert, decimal AlertQuantity);
}