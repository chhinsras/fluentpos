// --------------------------------------------------------------------------------------------------
// <copyright file="ProductRegisteredEvent.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Catalog.Core.Features.Products.Events
{
    public class ProductRegisteredEvent : Event
    {
        public Guid Id { get; }

        public string Name { get; }

        public string LocaleName { get; }

        public Guid BrandId { get; }

        public Guid CategoryId { get; }

        public decimal Price { get; }

        public decimal Cost { get; }

        public string ImageUrl { get; }

        public decimal Tax { get; }

        public string TaxMethod { get; }

        public string BarcodeSymbology { get; }

        public bool IsAlert { get; }

        public decimal AlertQuantity { get; }

        public string Detail { get; }

        public ProductRegisteredEvent(Product product)
        {
            Id = product.Id;
            Name = product.Name;
            LocaleName = product.LocaleName;
            BrandId = product.BrandId;
            CategoryId = product.CategoryId;
            Price = product.Price;
            Cost = product.Cost;
            ImageUrl = product.ImageUrl;
            Tax = product.Tax;
            TaxMethod = product.TaxMethod;
            BarcodeSymbology = product.BarcodeSymbology;
            IsAlert = product.IsAlert;
            AlertQuantity = product.AlertQuantity;
            Detail = product.Detail;
            AggregateId = product.Id;
            RelatedEntities = new[] { typeof(Product) };
        }
    }
}