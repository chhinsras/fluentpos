// --------------------------------------------------------------------------------------------------
// <copyright file="ISalesDbContext.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Sales.Core.Entities;
using Microsoft.EntityFrameworkCore;
using FluentPOS.Shared.Core.Interfaces;

namespace FluentPOS.Modules.Sales.Core.Abstractions
{
    public interface ISalesDbContext : IDbContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Transaction> Transactions { get; set; }
    }
}