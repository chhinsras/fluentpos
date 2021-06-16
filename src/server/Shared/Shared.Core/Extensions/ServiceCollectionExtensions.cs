using FluentPOS.Shared.Core.Behaviors;
using FluentPOS.Shared.Core.Settings;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FluentPOS.Shared.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSharedApplication(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<CacheSettings>(config.GetSection(nameof(CacheSettings)));
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            return services;
        }
    }
}