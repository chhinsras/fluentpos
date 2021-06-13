using FluentPOS.Modules.Catalog;
using FluentPOS.Modules.Identity.Extensions;
using FluentPOS.Shared.Abstractions.Extensions;
using FluentPOS.Shared.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Bootstrapper
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            _config = config;
        }
        public IConfiguration _config { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDistributedMemoryCache()
                 .AddSharedInfrastructure(_config)
                 .AddSharedApplication(_config)
                 .AddCatalogModule(_config)
                 .AddIdentityModule(_config);


        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSharedInfrastructure();

        }
    }
}