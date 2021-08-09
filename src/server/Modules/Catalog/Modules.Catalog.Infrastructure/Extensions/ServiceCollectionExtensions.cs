// --------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Reflection;
using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Infrastructure.Persistence;
using FluentPOS.Modules.Catalog.Infrastructure.Services;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Core.IntegrationServices.Catalog;
using FluentPOS.Shared.Infrastructure.Extensions;
using FluentPOS.Shared.Infrastructure.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.Catalog.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services)
        {
            services
                .AddDatabaseContext<CatalogDbContext>()
                .AddScoped<ICatalogDbContext>(provider => provider.GetService<CatalogDbContext>());
            services.AddExtendedAttributeDbContextsFromAssembly(typeof(CatalogDbContext), Assembly.GetAssembly(typeof(ICatalogDbContext)));
            services.AddTransient<IDatabaseSeeder, CatalogDbSeeder>();
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient<IProductService, ProductService>();
            return services;
        }

        public static IServiceCollection AddCatalogValidation(this IServiceCollection services)
        {
            services.AddControllers().AddCatalogValidation();
            return services;
        }
    }
}