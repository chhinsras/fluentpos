// --------------------------------------------------------------------------------------------------
// <copyright file="ModuleExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.People.Core.Extensions;
using FluentPOS.Modules.People.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.People.Extensions
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddPeopleModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddPeopleCore()
                .AddPeopleInfrastructure()
                .AddPeopleValidation();
            return services;
        }

        public static IApplicationBuilder UsePeopleModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}