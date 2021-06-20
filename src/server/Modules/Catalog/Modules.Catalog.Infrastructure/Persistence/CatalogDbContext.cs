using FluentPOS.Modules.Catalog.Core.Abstractions;
using FluentPOS.Modules.Catalog.Core.Entities;
using FluentPOS.Modules.Catalog.Infrastructure.Extensions;
using FluentPOS.Shared.Core.EventLogging;
using FluentPOS.Shared.Core.Interfaces;
using FluentPOS.Shared.Core.Settings;
using FluentPOS.Shared.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FluentPOS.Modules.Catalog.Infrastructure.Persistence
{
    public class CatalogDbContext : ModuleDbContext, ICatalogDbContext,
        IExtendedAttributeDbContext<Brand, BrandExtendedAttribute>,
        IExtendedAttributeDbContext<Category, CategoryExtendedAttribute>
    {
        private readonly PersistenceSettings _persistenceOptions;

        public CatalogDbContext(
            DbContextOptions<CatalogDbContext> options,
            IMediator mediator,
            IEventLogger eventLogger,
            IOptions<PersistenceSettings> persistenceOptions)
                : base(options, mediator, eventLogger, persistenceOptions)
        {
            _persistenceOptions = persistenceOptions.Value;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Catalog");
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyCatalogConfiguration(_persistenceOptions);
        }

        DbSet<Brand> IExtendedAttributeDbContext<Brand, BrandExtendedAttribute>.GetEntities() => Brands;
        DbSet<Category> IExtendedAttributeDbContext<Category, CategoryExtendedAttribute>.GetEntities() => Categories;
        DbSet<BrandExtendedAttribute> IExtendedAttributeDbContext<Brand, BrandExtendedAttribute>.ExtendedAttributes { get; set; }
        DbSet<CategoryExtendedAttribute> IExtendedAttributeDbContext<Category, CategoryExtendedAttribute>.ExtendedAttributes { get; set; }
    }
}