using FluentPOS.Modules.People.Core.Extensions;
using FluentPOS.Modules.People.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Modules.People.Extensions
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddPeopleModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddPeopleCore()
                .AddPeopleInfrastructure()
                .AddPeopleValidation();
            return services;
        }

        public static IApplicationBuilder UsePeopleModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}