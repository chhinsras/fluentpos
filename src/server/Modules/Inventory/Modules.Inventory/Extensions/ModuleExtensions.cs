// --------------------------------------------------------------------------------------------------
// <copyright file="ModuleExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Inventory.Core.Extensions;
using FluentPOS.Modules.Inventory.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.Inventory.Extensions
{
    /// <summary>
    /// Module Extensions.
    /// </summary>
    public static class ModuleExtensions
    {
        /// <summary>
        /// Service Registration for Inventory Module.
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/>.</param>
        /// <param name="configuration"><see cref="IConfiguration"/>.</param>
        /// <returns>Returns <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddInventoryModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddInventoryCore()
                .AddInventoryInfrastructure();
            return services;
        }

        /// <summary>
        /// Application Usage for Inventory Module.
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/>.</param>
        /// <returns>Returns <see cref="IApplicationBuilder"/>.</returns>
        public static IApplicationBuilder UseInventoryModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}