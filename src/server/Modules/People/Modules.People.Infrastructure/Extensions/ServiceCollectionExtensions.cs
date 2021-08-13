// --------------------------------------------------------------------------------------------------
// <copyright file="ServiceCollectionExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.Reflection;
using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Infrastructure.Persistence;
using FluentPOS.Modules.People.Infrastructure.Services;
using FluentPOS.Shared.Core.IntegrationServices.People;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Infrastructure.Extensions;
using FluentPOS.Shared.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.People.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPeopleInfrastructure(this IServiceCollection services)
        {
            services
                 .AddDatabaseContext<PeopleDbContext>()
                 .AddScoped<IPeopleDbContext>(provider => provider.GetService<PeopleDbContext>());
            services.AddExtendedAttributeDbContextsFromAssembly(typeof(PeopleDbContext), Assembly.GetAssembly(typeof(IPeopleDbContext)));
            services.AddTransient<IDatabaseSeeder, PeopleDbSeeder>();
            services.AddTransient<ICartService, CartService>();
            return services;
        }

        public static IServiceCollection AddPeopleValidation(this IServiceCollection services)
        {
            services.AddControllers().AddPeopleValidation();
            return services;
        }
    }
}