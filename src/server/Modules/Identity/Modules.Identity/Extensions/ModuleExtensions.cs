// --------------------------------------------------------------------------------------------------
// <copyright file="ModuleExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using FluentPOS.Modules.Identity.Core.Extensions;
using FluentPOS.Modules.Identity.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.Identity.Extensions
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddIdentityModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddIdentityCore()
                .AddIdentityInfrastructure(configuration);
            return services;
        }

        public static IApplicationBuilder UseIdentityModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}