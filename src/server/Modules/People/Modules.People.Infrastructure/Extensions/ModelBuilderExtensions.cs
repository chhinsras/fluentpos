using FluentPOS.Modules.People.Core.Entities;
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
                entity.ToTable("CustomerExtendedAttributes", "People");

                if (persistenceOptions.UseMsSql)
                {
                    entity.Property(p => p.Decimal)
                        .HasColumnType("decimal(23, 2)");
                    //entity.HasOne(p => p.Entity)
                    //    .WithMany(p => p.ExtendedAttributes)
                    //    .HasForeignKey(p => p.EntityId)
                    //    .OnDelete(DeleteBehavior.Cascade);
                }
            });
        }
    }
}