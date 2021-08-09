// --------------------------------------------------------------------------------------------------
// <copyright file="ICatalogDbContext.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Modules.Catalog.Core.Abstractions
{
    public interface ICatalogDbContext : IDbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<Brand> Brands { get; set; }

        public DbSet<Category> Categories { get; set; }
    }
}