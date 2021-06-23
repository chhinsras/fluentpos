using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Shared.Core.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Modules.Identity.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyIdentityConfiguration(this ModelBuilder builder, PersistenceSettings persistenceOptions)
        {
            // build model for MSSQL and Postgres

            builder.Entity<FluentUser>(entity =>
            {
                entity.ToTable(name: "Users");
            });
            builder.Entity<FluentRole>(entity =>
            {
                entity.ToTable(name: "Roles");
            });
            builder.Entity<FluentRoleClaim>(entity =>
            {
                entity.ToTable(name: "RoleClaims");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });

            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

            builder.Entity<UserExtendedAttribute>(entity =>
            {
                entity.ToTable("UserExtendedAttributes");

                if (persistenceOptions.UseMsSql)
                {
                    entity.Property(p => p.Decimal)
                        .HasColumnType("decimal(23, 2)");
                }
            });
        }
    }
}