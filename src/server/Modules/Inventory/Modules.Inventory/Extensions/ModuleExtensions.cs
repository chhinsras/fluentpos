using FluentPOS.Modules.Inventory.Core.Extensions;
using FluentPOS.Modules.Inventory.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.Inventory.Extensions
{
    /// <summary>
    /// Module Extensions
    /// </summary>
    public static class ModuleExtensions
    {
        /// <summary>
        /// Service Registration for Inventory Module
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddInventoryModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddInventoryCore()
                .AddInventoryInfrastructure();
            return services;
        }
        /// <summary>
        /// Application Usage for Inventory Module
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseInventoryModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}