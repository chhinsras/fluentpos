using FluentPOS.Shared.Infrastructure.Extensions;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Shared.Infrastructure.Persistence.Postgres
{
    public static class Extensions
    {
        public static IServiceCollection AddPostgres<T>(this IServiceCollection services) where T : DbContext
        {
            var options = services.GetOptions<PersistenceSettings>(nameof(PersistenceSettings));
            var connectionString = options.ConnectionStrings.Postgres;
            services.AddDbContext<T>(m => m.UseNpgsql(connectionString, e => e.MigrationsAssembly(typeof(T).Assembly.FullName)));
            using var scope = services.BuildServiceProvider().CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<T>();
            dbContext.Database.Migrate();
            services.AddHangfire(x => x.UsePostgreSqlStorage(connectionString));
            return services;
        }
    }
}