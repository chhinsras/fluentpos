using FluentPOS.Shared.Core.Domain;
using System;
using System.Collections.Generic;

namespace FluentPOS.Modules.Catalog.Core.Entities
{
    public class Product : BaseEntity //IAuditableEntity
    {
        public string Name { get; set; }
        public string LocaleName { get; set; }
        public Guid BrandId { get; set; }
        public virtual Brand Brand { get; set; }
        public Guid CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public string ImageUrl { get; set; }
        public string Tax { get; set; }
        public string TaxMethod { get; set; }
        public string BarcodeSymbology { get; set; }
        public bool IsAlert { get; set; }
        public decimal AlertQuantity { get; set; }
        public string Detail { get; set; }

        public virtual ICollection<ProductExtendedAttribute> ExtendedAttributes { get; set; }

        public Product() : base()
        {
            ExtendedAttributes = new HashSet<ProductExtendedAttribute>();
        }
    }
}