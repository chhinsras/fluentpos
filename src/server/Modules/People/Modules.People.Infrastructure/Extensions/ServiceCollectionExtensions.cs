using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Infrastructure.Persistence;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Infrastructure.Extensions;
using FluentPOS.Shared.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FluentPOS.Modules.People.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPeopleInfrastructure(this IServiceCollection services)
        {
            services
                 .AddDatabaseContext<PeopleDbContext>()
                 .AddScoped<IPeopleDbContext>(provider => provider.GetService<PeopleDbContext>());
            services.AddExtendedAttributeDbContextsFromAssembly(typeof(PeopleDbContext), Assembly.GetAssembly(typeof(IPeopleDbContext)));
            services.AddTransient<IDatabaseSeeder, PeopleDbSeeder>();
            return services;
        }

        public static IServiceCollection AddPeopleValidation(this IServiceCollection services)
        {
            services.AddControllers().AddPeopleValidation();
            return services;
        }
    }
}