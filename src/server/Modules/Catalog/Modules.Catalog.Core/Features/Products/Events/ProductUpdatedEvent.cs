using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Events
{
    public class ProductUpdatedEvent : Event
    {
        public ProductUpdatedEvent(Guid id, string name, string localeName, Guid brandId, Guid categoryId, decimal price, decimal cost, string imageUrl, string tax, string taxMethod, string barcodeSymbology, bool isAlert, decimal alertQuantity, string detail)
        {
            Id = id;
            Name = name;
            LocaleName = localeName;
            BrandId = brandId;
            CategoryId = categoryId;
            Price = price;
            Cost = cost;
            ImageUrl = imageUrl;
            Tax = tax;
            TaxMethod = taxMethod;
            BarcodeSymbology = barcodeSymbology;
            IsAlert = isAlert;
            AlertQuantity = alertQuantity;
            Detail = detail;
            AggregateId = id;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string LocaleName { get; }
        public Guid BrandId { get; }
        public Guid CategoryId { get; }
        public decimal Price { get; }
        public decimal Cost { get; }
        public string ImageUrl { get; }
        public string Tax { get; }
        public string TaxMethod { get; }
        public string BarcodeSymbology { get; }
        public bool IsAlert { get; }
        public decimal AlertQuantity { get; }
        public string Detail { get; }
    }
}