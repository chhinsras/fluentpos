// --------------------------------------------------------------------------------------------------
// <copyright file="ModuleExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Sales.Core.Extensions;
using FluentPOS.Modules.Sales.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.Sales.Extensions
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddSalesModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSalesCore()
                .AddSalesInfrastructure()
                .AddSalesValidation();
            return services;
        }

        public static IApplicationBuilder UseSalesModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}