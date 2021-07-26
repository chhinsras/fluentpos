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

            builder.Entity<CustomerExtendedAttribute>(entity =>
            {
                entity.ToTable("CustomerExtendedAttributes");

                if (persistenceOptions.UseMsSql)
                {
                    entity.Property(p => p.Decimal)
                        .HasColumnType("decimal(23, 2)");
                }
            });

            builder.Entity<CartExtendedAttribute>(entity =>
            {
                entity.ToTable("CartExtendedAttributes");

                if (persistenceOptions.UseMsSql)
                {
                    entity.Property(p => p.Decimal)
                        .HasColumnType("decimal(23, 2)");
                }
            });
        }
    }
}