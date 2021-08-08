using FluentPOS.Modules.Sales.Core.Abstractions;
using FluentPOS.Modules.Sales.Infrastructure.Persistence;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Infrastructure.Extensions;
using FluentPOS.Shared.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FluentPOS.Modules.Sales.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSalesInfrastructure(this IServiceCollection services)
        {
            services
                 .AddDatabaseContext<SalesDbContext>()
                 .AddScoped<ISalesDbContext>(provider => provider.GetService<SalesDbContext>());
            services.AddExtendedAttributeDbContextsFromAssembly(typeof(SalesDbContext), Assembly.GetAssembly(typeof(ISalesDbContext)));
            return services;
        }

        public static IServiceCollection AddSalesValidation(this IServiceCollection services)
        {
            services.AddControllers().AddSalesValidation();
            return services;
        }
    }
}