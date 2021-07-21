using FluentPOS.Modules.Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using FluentPOS.Shared.Core.Interfaces;

namespace FluentPOS.Modules.Catalog.Core.Abstractions
{
    public interface ICatalogDbContext : IDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}