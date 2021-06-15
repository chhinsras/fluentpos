using FluentPOS.Shared.Infrastructure.Persistence.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FluentPOS.Shared.Infrastructure.Persistence
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabaseContext<T>(this IServiceCollection services) where T : DbContext
        {
            services.AddPostgres<T>();
            //switch to msssql if needed here
            return services;
        }
    }
}