using FluentPOS.Modules.People.Core.Extensions;
using FluentPOS.Modules.People.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentPOS.Modules.People.Extensions
{
    public static class ModuleExtensions
    {
        public static IServiceCollection AddPeopleModule(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddPeopleCore()
                .AddPeopleInfrastructure();
            return services;
        }

        public static IApplicationBuilder UsePeopleModule(this IApplicationBuilder app)
        {
            return app;
        }
    }
}
