using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Shared.Core.Settings;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Modules.Catalog.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyCatalogConfiguration(this ModelBuilder builder, PersistenceSettings persistenceOptions)
        {
            // build model for MSSQL and Postgres

            builder.Entity<Product>(entity =>
            {
                entity.ToTable(name: "Products", "Catalog");

                if (persistenceOptions.UseMsSql)
                {
                    entity.Property(p => p.Price)
                        .HasColumnType("decimal(23, 2)");
                    entity.Property(p => p.Cost)
                        .HasColumnType("decimal(23, 2)");
                    entity.Property(p => p.AlertQuantity)
                        .HasColumnType("decimal(23, 2)");
                }
            });

            builder.Entity<BrandExtendedAttribute>(entity =>
            {
                entity.ToTable("BrandExtendedAttributes", "Catalog");

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

            builder.Entity<CategoryExtendedAttribute>(entity =>
            {
                entity.ToTable("CategoryExtendedAttributes", "Catalog");

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

            builder.Entity<ProductExtendedAttribute>(entity =>
            {
                entity.ToTable("ProductExtendedAttributes", "Catalog");

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