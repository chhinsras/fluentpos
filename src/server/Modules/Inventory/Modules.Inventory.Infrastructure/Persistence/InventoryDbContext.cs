// --------------------------------------------------------------------------------------------------
// <copyright file="InventoryDbContext.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Inventory.Core.Abstractions;
using FluentPOS.Modules.Inventory.Core.Entities;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Interfaces.Serialization;
using FluentPOS.Shared.Core.Settings;
using FluentPOS.Shared.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FluentPOS.Modules.Inventory.Infrastructure.Persistence
{
    public class InventoryDbContext : ModuleDbContext, IInventoryDbContext
    {
        private readonly PersistenceSettings _persistenceOptions;
        private readonly IJsonSerializer _json;

        protected override string Schema => "Inventory";

        public InventoryDbContext(
            DbContextOptions<InventoryDbContext> options,
            IMediator mediator,
            IEventLogger eventLogger,
            IOptions<PersistenceSettings> persistenceOptions,
            IJsonSerializer json)
                : base(options, mediator, eventLogger, persistenceOptions, json)
        {
            _persistenceOptions = persistenceOptions.Value;
            _json = json;
        }

        public DbSet<Stock> Stocks { get; set; }

        public DbSet<StockTransaction> StockTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}