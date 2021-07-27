using System.Linq;
using FluentPOS.Modules.People.Core.Entities.ExtendedAttributes;
using FluentPOS.Shared.Core.Settings;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Modules.People.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyPeopleConfiguration(this ModelBuilder builder, PersistenceSettings persistenceOptions)
        {
            // build model for MSSQL and Postgres

            if (persistenceOptions.UseMsSql)
            {
                foreach (var property in builder.Model.GetEntityTypes()
                    .SelectMany(t => t.GetProperties())
                    .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
                {
                    property.SetColumnType("decimal(23,2)");
                }
            }

            builder.Entity<CustomerExtendedAttribute>(entity =>
            {
                entity.ToTable("CustomerExtendedAttributes");
            });

            builder.Entity<CartExtendedAttribute>(entity =>
            {
                entity.ToTable("CartExtendedAttributes");
            });

            builder.Entity<CartItemExtendedAttribute>(entity =>
            {
                entity.ToTable("CartItemExtendedAttributes");
            });
        }
    }
}