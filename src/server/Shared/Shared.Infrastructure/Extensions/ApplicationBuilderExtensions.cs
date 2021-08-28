// --------------------------------------------------------------------------------------------------
// <copyright file="ApplicationBuilderExtensions.cs" company="FluentPOS">
// Copyright (c) FluentPOS. All rights reserved.
// The core team: Mukesh Murugan (iammukeshm), Chhin Sras (chhinsras), Nikolay Chebotov (unchase).
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>
// --------------------------------------------------------------------------------------------------

using System.IO;
using System.Runtime.CompilerServices;
using FluentPOS.Shared.Core.Interfaces.Services;
using FluentPOS.Shared.Infrastructure.Middlewares;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Swashbuckle.AspNetCore.SwaggerUI;

[assembly: InternalsVisibleTo("FluentPOS.Bootstrapper")]

namespace FluentPOS.Shared.Infrastructure.Extensions
{
    internal static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseSharedInfrastructure(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandler>();
            app.UseRouting();

            string filesDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "Files");
            if (!Directory.Exists(filesDirectoryPath))
            {
                Directory.CreateDirectory(filesDirectoryPath);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Files")),
                RequestPath = "/files"
            });
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHangfireDashboard("/jobs", new DashboardOptions
            {
                DashboardTitle = "FluentPOS Jobs"
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwaggerDocumentation();
            app.Initialize();

            return app;
        }

        internal static IApplicationBuilder Initialize(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var initializers = serviceScope.ServiceProvider.GetServices<IDatabaseSeeder>();

            foreach (var initializer in initializers)
            {
                initializer.Initialize();
            }

            return app;
        }

        private static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.DefaultModelsExpandDepth(-1);
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
                options.RoutePrefix = "swagger";
                options.DisplayRequestDuration();
                options.DocExpansion(DocExpansion.None);
            });
            return app;
        }
    }
}