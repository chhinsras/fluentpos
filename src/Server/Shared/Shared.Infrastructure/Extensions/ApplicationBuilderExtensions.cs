using FluentPOS.Shared.Infrastructure.Middlewares;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FluentPOS.Bootstrapper")]

namespace FluentPOS.Shared.Infrastructure.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSharedInfrastructure(this IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseMiddleware<GlobalExceptionHandler>();
            app.UseSwaggerDocumentation();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                DashboardTitle = "FluentPOS Jobs"
            });
            return app;
        }

        private static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DefaultModelsExpandDepth(-1);
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = "swagger";
                options.DisplayRequestDuration();
            });
            return app;
        }
    }
}