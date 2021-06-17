using FluentPOS.Modules.People.Core.Abstractions;
using FluentPOS.Modules.People.Infrastructure.Persistence;
using FluentPOS.Shared.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddPeopleInfrastructure(this IServiceCollection services)
        {
            services
                 .AddDatabaseContext<PeopleDbContext>()
                 .AddScoped<IPeopleDbContext>(provider => provider.GetService<PeopleDbContext>());
           // services.AddTransient<IDatabaseSeeder, CatalogDbSeeder>();
            return services;
        }
    }
}
