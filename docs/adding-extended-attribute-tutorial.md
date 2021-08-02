**Synonims:**

* |ea-type-name| - the extended attribute class name;
* |module-name| - the name of the module for adding the new extended attribute;
* |related-entity-type-name| - the type of entity related with the extended attribute.

*Examples in all steps use:*

|ea-type-name| = `BrandExtendedAttribute`

|module-name| = `Catalog`

|related-entity-id-type| = `Guid`

|related-entity-type-name| = `Brand`

**Steps:**

1) Add an extended attribute entity class named |ea-type-name| into **FluentPOS.Modules.|module-name|.Core.Entities** inherited from the abstract class `ExtendedAttribute<|related-entity-id-type|, |related-entity-type-name|>`:

```csharp
using FluentPOS.Shared.Core.Domain;

namespace FluentPOS.Modules.Catalog.Core.Entities
{
    public class BrandExtendedAttribute : ExtendedAttribute<Guid, Brand> { }
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

3) Implement `IExtendedAttributeDbContext<|related-entity-id-type|, |related-entity-type-name|, |ea-type-name|>` in database context class:

```csharp
public class CatalogDbContext : ModuleDbContext, ICatalogDbContext,
    IExtendedAttributeDbContext<Guid, Brand, BrandExtendedAttribute>
{
    // fields and constructor...

    public DbSet<Brand> Brands { get; set; }
    // other DbSets...

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
	    //...
	    modelBuilder.ApplyCatalogConfiguration(_persistenceOptions); // see step 4.
    }

    DbSet<Brand> IExtendedAttributeDbContext<Guid, Brand, BrandExtendedAttribute>.GetEntities() => Brands;
    DbSet<BrandExtendedAttribute> IExtendedAttributeDbContext<Guid, Brand, BrandExtendedAttribute>.ExtendedAttributes { get; set; }
}
```

4) Into the `Apply|module-name|Configuration` extension method add this code for resolving types for MSSQL:

```csharp
public static void ApplyCatalogConfiguration(this ModelBuilder builder, PersistenceSettings persistenceOptions)
{
    if (persistenceOptions.UseMsSql)
    {
        foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(23,2)");
        }
    }

    //...

    builder.Entity<BrandExtendedAttribute>(entity =>
    {
        entity.ToTable("BrandExtendedAttributes", "Catalog");
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
    [ApiVersion("1")]
    [Route(BaseController.BasePath + "/" + nameof(Brand) + "/attributes")]
    public class BrandExtendedAttributesController : ExtendedAttributesController<Guid, Brand>
    {
	    private IMediator _mediatorInstance;
	    protected override IMediator Mediator => _mediatorInstance ??= HttpContext.RequestServices.GetService<IMediator>();

	    [Authorize(Policy = Permissions.BrandsExtendedAttributes.ViewAll)]
	    public override Task<IActionResult> GetAll(PaginatedExtendedAttributeFilter<Guid> filter)
	    {
		    return base.GetAll(filter);
	    }

	    [Authorize(Policy = Permissions.BrandsExtendedAttributes.View)]
	    public override Task<IActionResult> GetById(Guid id, bool bypassCache)
	    {
		    return base.GetById(id, bypassCache);
	    }

	    [Authorize(Policy = Permissions.BrandsExtendedAttributes.Add)]
	    public override Task<IActionResult> Create(AddExtendedAttributeCommand<Guid, Brand> command)
	    {
		    return base.Create(command);
	    }

	    [Authorize(Policy = Permissions.BrandsExtendedAttributes.Update)]
	    public override Task<IActionResult> Update(UpdateExtendedAttributeCommand<Guid, Brand> command)
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

8) Add `AddExtendedAttributeDbContextsFromAssembly` extension method in `ServiceCollectionExtensions` for |module-name| to register extended attribute handlers automatically for this module:

```csharp
namespace FluentPOS.Modules.Catalog.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogInfrastructure(this IServiceCollection services)
        {
            services
                .AddDatabaseContext<CatalogDbContext>()
                .AddScoped<ICatalogDbContext>(provider => provider.GetService<CatalogDbContext>());
            services.AddExtendedAttributeDbContextsFromAssembly(typeof(CatalogDbContext), Assembly.GetAssembly(typeof(ICatalogDbContext)));
            services.AddTransient<IDatabaseSeeder, CatalogDbSeeder>();
            return services;
        }
    }
}
```

9) Add validators for added extended attribute into **FluentPOS.Modules.|module-name|.Core.*.Validators** inherited from each the abstract classes: `AddExtendedAttributeCommandValidator<|related-entity-id-type|, |related-entity-type-name|>`, `UpdateExtendedAttributeCommandValidator<|related-entity-id-type|, |related-entity-type-name|>` and `RemoveExtendedAttributeCommandValidator<|related-entity-id-type|, |related-entity-type-name|>`:

```csharp
namespace FluentPOS.Modules.Catalog.Core.Features.ExtendedAttributes.Validators.Brands
{
    public class AddBrandExtendedAttributeCommandValidator : AddExtendedAttributeCommandValidator<Guid, Brand>
    {
        public AddBrandExtendedAttributeCommandValidator(IStringLocalizer<AddBrandExtendedAttributeCommandValidator> localizer, IJsonSerializer jsonSerializer) : base(localizer, jsonSerializer)
        {
            // you can override the validation rules here
        }
    }
}
```

```csharp
namespace FluentPOS.Modules.Catalog.Core.Features.ExtendedAttributes.Validators.Brands
{
    public class UpdateBrandExtendedAttributeCommandValidator : UpdateExtendedAttributeCommandValidator<Guid, Brand>
    {
        public UpdateBrandExtendedAttributeCommandValidator(IStringLocalizer<UpdateBrandExtendedAttributeCommandValidator> localizer, IJsonSerializer jsonSerializer) : base(localizer, jsonSerializer)
        {
            // you can override the validation rules here
        }
    }
}
```

```csharp
namespace FluentPOS.Modules.Catalog.Core.Features.ExtendedAttributes.Validators.Brands
{
    public class RemoveBrandExtendedAttributeCommandValidator : RemoveExtendedAttributeCommandValidator<Guid, Brand>
    {
        public RemoveBrandExtendedAttributeCommandValidator(IStringLocalizer<RemoveBrandExtendedAttributeCommandValidator> localizer, IJsonSerializer jsonSerializer) : base(localizer, jsonSerializer)
        {
            // you can override the validation rules here
        }
    }
}
```

```csharp
namespace FluentPOS.Modules.Catalog.Core.Features.ExtendedAttributes.Validators.Brands
{
    public class PaginatedBrandExtendedAttributeFilterValidator : PaginatedExtendedAttributeFilterValidator<Guid, Brand>
    {
        public BrandPaginatedExtendedAttributeFilterValidator(IStringLocalizer<BrandPaginatedExtendedAttributeFilterValidator> localizer) : base(localizer)
        {
            // you can override the validation rules here
        }
    }
}
```

10) Add `AddExtendedAttributeHandlersFromAssembly` and `AddExtendedAttributeCommandValidatorsFromAssembly` extension methods in `ServiceCollectionExtensions` for |module-name| to register extended attribute handlers and handler validators automatically for this module:

```csharp
namespace FluentPOS.Modules.Catalog.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCatalogCore(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddExtendedAttributeHandlersFromAssembly(Assembly.GetExecutingAssembly());
            services.AddExtendedAttributeCommandValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddPaginatedExtendedAttributeFilterValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddPaginatedFilterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
```

11) Add the database migration using terminal: 

```
dotnet ef migrations add "initial" --startup-project ../../../API -o Persistence/Migrations/ --context CatalogDbContext
```

Or using *Package Manager Console* in Visual Studio (should change *Default project* to **Modules.Catalog.Infrastructure**):

```
add-migration initial -o Persistence/Migrations/ -context CatalogDbContext
```