// --------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Inventory.Core.Abstractions;
using FluentPOS.Modules.Inventory.Infrastructure.Persistence;
using FluentPOS.Modules.Inventory.Infrastructure.Services;
using FluentPOS.Shared.Core.IntegrationServices.Inventory;
using FluentPOS.Shared.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.Inventory.Infrastructure.Extensions
{
    /// <summary>
    /// Service Collection Extension.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Persistence Registration for Inventory Module.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddInventoryInfrastructure(this IServiceCollection services)
        {
            services
                 .AddDatabaseContext<InventoryDbContext>()
                 .AddScoped<IInventoryDbContext>(provider => provider.GetService<InventoryDbContext>());
            services.AddTransient<IStockService, StockService>();
            return services;
        }
    }
}