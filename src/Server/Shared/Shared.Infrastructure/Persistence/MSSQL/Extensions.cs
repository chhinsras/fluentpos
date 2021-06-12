using FluentPOS.Shared.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Shared.Infrastructure.Persistence.MSSQL
{
    public static class Extensions
    {
        internal static IServiceCollection AddMSSQL(this IServiceCollection services)
        {
            var options = services.GetOptions<MSSQLSettings>("mssql");
            services.AddSingleton(options);

            return services;
        }

        public static IServiceCollection AddMSSQL<T>(this IServiceCollection services) where T : DbContext
        {
            var options = services.GetOptions<MSSQLSettings>("mssql");
            services.AddDbContext<T>(m => m.UseSqlServer(options.ConnectionString, e => e.MigrationsAssembly(typeof(T).Assembly.FullName)));
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<T>();
            dbContext.Database.Migrate();
            return services;
        }
    }
}