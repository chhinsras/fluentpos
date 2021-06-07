using FluentPOS.Shared.Abstractions.Domain;

namespace FluentPOS.Modules.Catalog.Core.Entites
{
    public class Product : BaseEntity //IAuditableEntity
    {
        public string Name { get; set; }
        public string LocaleName { get; set; }
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public string ImageUrl { get; set; }
        public string Tax { get; set; }
        public string TaxMethod { get; set; }
        public string BarcodeSymbology { get; set; }
        public decimal AlertQuantity { get; set; }
        public string Detail { get; set; }
    }
}
