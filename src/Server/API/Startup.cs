using FluentPOS.Modules.Catalog;
using FluentPOS.Shared.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Bootstrapper
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSharedInfrastructure()
                .AddCatalogModule();
                
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSharedInfrastructure();
            
        }
    }
}