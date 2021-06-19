using FluentPOS.Modules.Identity.Core.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.Identity.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPermissions(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>()
                .AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            return services;
        }
    }
}