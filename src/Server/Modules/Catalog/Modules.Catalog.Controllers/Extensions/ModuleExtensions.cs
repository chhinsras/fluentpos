using FluentPOS.Modules.Catalog.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.Catalog
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection services)
        {
            services.AddCatalogInfrastructure();
            return services;
        }

        public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}