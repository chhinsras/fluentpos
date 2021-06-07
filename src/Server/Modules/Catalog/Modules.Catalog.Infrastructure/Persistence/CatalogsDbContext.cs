using FluentPOS.Modules.Catalog.Core.Entites;
using Microsoft.EntityFrameworkCore;

namespace FluentPOS.Modules.Catalog.Infrastructure.Persistence
{
    public class CatalogDbContext : DbContext
    {
        public CatalogDbContext(DbContextOptions options) : base(options)
        {}
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Catalog");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
