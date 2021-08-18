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
        /// Persistance Registration for Inventory Module.
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
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