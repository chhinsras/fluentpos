using FluentPOS.Shared.Core.Domain;
using System;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Events
{
    public class ProductRegisteredEvent : Event
    {
        public ProductRegisteredEvent(Guid id, string name, string localeName, Guid brandId, Guid categoryId, decimal price, decimal cost, string imageUrl, string tax, string taxMethod, string barcodeSymbology, bool isAlert, decimal alertQuantity, string detail)
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

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LocaleName { get; set; }
        public Guid BrandId { get; set; }
        public Guid CategoryId { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public string ImageUrl { get; set; }
        public string Tax { get; set; }
        public string TaxMethod { get; set; }
        public string BarcodeSymbology { get; set; }
        public bool IsAlert { get; set; }
        public decimal AlertQuantity { get; set; }
        public string Detail { get; set; }
    }
}