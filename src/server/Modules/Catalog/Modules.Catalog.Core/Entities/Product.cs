// --------------------------------------------------------------------------------------------------
// <copyright file="Product.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using FluentPOS.Modules.Catalog.Core.Entities.ExtendedAttributes;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Catalog.Core.Entities
{
    public class Product : BaseEntity
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

        public decimal Tax { get; set; }

        public string TaxMethod { get; set; }

        public string BarcodeSymbology { get; set; }

        public bool IsAlert { get; set; }

        public decimal AlertQuantity { get; set; }

        public string Detail { get; set; }

        public virtual ICollection<ProductExtendedAttribute> ExtendedAttributes { get; set; }

        public Product()
            : base()
        {
            ExtendedAttributes = new HashSet<ProductExtendedAttribute>();
        }
    }
}