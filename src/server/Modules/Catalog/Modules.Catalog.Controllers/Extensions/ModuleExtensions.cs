using FluentPOS.Modules.Catalog.Core.Extensions;
using FluentPOS.Modules.Catalog.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.Catalog.Extensions
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddCatalogModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddCatalogCore()
                .AddCatalogInfrastructure()
                .AddCatalogValidation();
            return services;
        }

        public static IApplicationBuilder UseCatalogModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}