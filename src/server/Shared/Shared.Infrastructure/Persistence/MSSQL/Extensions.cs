using FluentPOS.Shared.Infrastructure.Extensions;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Shared.Infrastructure.Persistence.MSSQL
{
    public static class Extensions
    {
        public static IServiceCollection AddMSSQL<T>(this IServiceCollection services) where T : DbContext
        {
            var options = services.GetOptions<PersistenceSettings>(nameof(PersistenceSettings));
            var connectionString = options.ConnectionStrings.MSSQL;
            services.AddDbContext<T>(m => m.UseSqlServer(connectionString, e => e.MigrationsAssembly(typeof(T).Assembly.FullName)));
            services.AddHangfire(x => x.UseSqlServerStorage(connectionString));
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<T>();
            dbContext.Database.Migrate();

            return services;
        }
    }
}