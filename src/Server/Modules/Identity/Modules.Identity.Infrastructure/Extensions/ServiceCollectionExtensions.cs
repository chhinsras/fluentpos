using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Modules.Identity.Core.Settings;
using FluentPOS.Modules.Identity.Infrastructure.Persistence;
using FluentPOS.Modules.Identity.Infrastructure.Services;
using FluentPOS.Shared.Abstractions.Identity;
using FluentPOS.Shared.Infrastructure.Persistence.Postgres;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.Identity.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
            services.AddTransient<ITokenService, TokenService>();
            services.AddPostgres<IdentityDbContext>();
            services.AddIdentity<ExtendedIdentityUser, ExtendedIdentityRole>().AddEntityFrameworkStores<IdentityDbContext>().AddDefaultTokenProviders();
            return services;
        }
    }
}
