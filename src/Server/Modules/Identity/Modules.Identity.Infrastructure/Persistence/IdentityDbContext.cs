using FluentPOS.Modules.Identity.Core.Entities;
using FluentPOS.Modules.Identity.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Modules.Identity.Infrastructure.Persistence
{
    public class IdentityDbContext : IdentityDbContext<ExtendedIdentityUser, ExtendedIdentityRole, int>
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            modelBuilder.ApplyIdentityConfiguration();
        }
    }
}