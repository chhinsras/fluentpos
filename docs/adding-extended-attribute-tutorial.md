**Synonims:**

* |ea-type-name| - the extended attribute class name;
* |module-name| - the name of the module for adding the new extended attribute;
* |related-entity-type-name| - the type of entity related with the extended attribute.

*Examples in all steps use:*

|ea-type-name| = `BrandExtendedAttribute`

|module-name| = `Catalog`

|related-entity-type-name| = `Brand`

**Steps:**

1) Add an extended attribute entity class named |ea-type-name| into **FluentPOS.Modules.|module-name|.Core.Entities** inherited from the abstract class `ExtendedAttribute<|related-entity-type-name|>`:

```csharp
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Catalog.Core.Entities
{
    public class BrandExtendedAttribute : ExtendedAttribute<Brand> { }
}
```

2) Add virtual property `public virtual ICollection<|ea-type-name|> ExtendedAttributes { get; set; }` into the |related-entity-type-name| class and initialize it in it's constrictor:

```csharp
namespace FluentPOS.Modules.Catalog.Core.Entities
{
    public class Brand : BaseEntity
    {
        // some properties
	    //...

        public virtual ICollection<BrandExtendedAttribute> ExtendedAttributes { get; set; }

        public Brand() : base()
        {
            ExtendedAttributes = new HashSet<BrandExtendedAttribute>();
        }
    }
}
```

3) Implement `IExtendedAttributeDbContext<|related-entity-type-name|, |ea-type-name|>` in database context class:

```csharp
public class CatalogDbContext : ModuleDbContext, ICatalogDbContext,
    IExtendedAttributeDbContext<Brand, BrandExtendedAttribute>
{
    // fields and constructor...

    public DbSet<Brand> Brands { get; set; }
    // other DbSets...

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
	    //...
	    modelBuilder.ApplyCatalogConfiguration(_persistenceOptions); // see step 4.
    }

    DbSet<Brand> IExtendedAttributeDbContext<Brand, BrandExtendedAttribute>.GetEntities() => Brands;
    DbSet<BrandExtendedAttribute> IExtendedAttributeDbContext<Brand, BrandExtendedAttribute>.ExtendedAttributes { get; set; }
}
```

4) Into the `Apply|module-name|Configuration` extension method add this code for resolving types for MSSQL:

```csharp
public static void ApplyCatalogConfiguration(this ModelBuilder builder, PersistenceSettings persistenceOptions)
{
    //...

    builder.Entity<BrandExtendedAttribute>(entity =>
    {
        entity.ToTable("BrandExtendedAttributes", "Catalog");

        if (persistenceOptions.UseMsSql)
        {
            entity.Property(p => p.Decimal)
                .HasColumnType("decimal(23, 2)");
        }
    });

    //...
}
```

5) Add the extended attributes profile into the module (for Automapper):

```csharp
namespace FluentPOS.Modules.Catalog.Core.Mappings
{
    public class ExtendedAttributesProfile : Profile
    {
        public ExtendedAttributesProfile()
        {
            this.CreateExtendedAttributesMappings(Assembly.GetExecutingAssembly());
        }
    }
}
```

6) Add permission constants for the extended attribute:

```csharp
namespace FluentPOS.Shared.Core.Constants
{
    public static class Permissions
    {
        //...

        public static class BrandsExtendedAttributes
        {
            public const string View = "Permissions.Brands.ExtendedAttributes.View";
            public const string ViewAll = "Permissions.Brands.ExtendedAttributes.ViewAll";
            public const string Add = "Permissions.Brands.ExtendedAttributes.Add";
            public const string Update = "Permissions.Brands.ExtendedAttributes.Update";
            public const string Remove = "Permissions.Brands.ExtendedAttributes.Remove";
        }

        // other constants...
    }
}
```

7) Add the extended attributes controller inherited from the abstract class `ExtendedAttributesController<|related-entity-type-name|>`:

```csharp
namespace FluentPOS.Modules.Catalog.Controllers
{
    [Route("api/catalog/" + nameof(Brand) + "/attributes")]
    public class BrandExtendedAttributesController : ExtendedAttributesController<Brand>
    {
	    private IMediator _mediatorInstance;
	    protected override IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

	    [Authorize(Policy = Permissions.BrandsExtendedAttributes.ViewAll)]
	    public override Task<IActionResult> GetAll(PaginatedExtendedAttributeFilter filter)
	    {
		    return base.GetAll(filter);
	    }

	    [Authorize(Policy = Permissions.BrandsExtendedAttributes.View)]
	    public override Task<IActionResult> GetById(Guid id, bool bypassCache)
	    {
		    return base.GetById(id, bypassCache);
	    }

	    [Authorize(Policy = Permissions.BrandsExtendedAttributes.Add)]
	    public override Task<IActionResult> Create(AddExtendedAttributeCommand<Brand> command)
	    {
		    return base.Create(command);
	    }

	    [Authorize(Policy = Permissions.BrandsExtendedAttributes.Update)]
	    public override Task<IActionResult> Update(UpdateExtendedAttributeCommand<Brand> command)
	    {
		    return base.Update(command);
	    }

	    [Authorize(Policy = Permissions.BrandsExtendedAttributes.Remove)]
	    public override Task<IActionResult> Remove(Guid id)
	    {
		    return base.Remove(id);
	    }
    }
}
```

Here you should add `Authorize` attributes to all needed actions, `Route` attribute to the controller.
You can change route templates for actions using `HttpGet`, `HttpPost`, `HttpPut` and `HttpDelete` attributes here.

8) Add the database migration using terminal: 

```
dotnet ef migrations add "initial" --startup-project ../../../API -o Persistence/Migrations/ --context CatalogDbContext
```