using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Infrastructure.Persistence;
using FluentPOS.Shared.Infrastructure.Persistence.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.Catalog.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services)
        {
            services.AddPostgres<CatalogDbContext>();
            services.AddScoped<ICatalogDbContext>(provider => provider.GetService<CatalogDbContext>());
            return services;
        }
    }
}