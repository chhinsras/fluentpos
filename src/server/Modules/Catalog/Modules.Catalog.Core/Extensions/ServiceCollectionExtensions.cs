using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentPOS.Shared.Core.Extensions;

namespace FluentPOS.Modules.Catalog.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogCore(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddExtendedAttributeHandlersFromAssembly(Assembly.GetExecutingAssembly());

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}