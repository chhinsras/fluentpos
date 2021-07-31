using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Infrastructure.Persistence;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Infrastructure.Extensions;
using FluentPOS.Shared.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
            return services;
        }

        public static IServiceCollection AddCatalogValidation(this IServiceCollection services)
        {
            services.AddControllers().AddCatalogValidation();
            return services;
        }
    }
}