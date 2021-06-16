# Documentation

###Switching Database Providers

fluentpos currently supports MSSQL & Postgres Dbs. Due to issues with schemas in EFCore's MySQL implementation, MySQL is not being used here.

Firstly, you need to make sure that valid connection strings are mentioned in the appSetting.json
Next, set either to true in appSetting under `PersistenceSettings`.

`"UseMsSql": true,`
`"UsePostgres": true,`

Note: If you set both to true, Postgres will be used by default.

### Important - While Switching DBProviders via EFcore, make sure to delete all the migrations, and re-add migrations via the below CLI Command.
### Navigate to `src\Modules\Modules.{Module_Name}.Infrastructure` and execute the following.


`dotnet ef migrations add {commitMessage} --startup-project ../../../API -o Persistence/Migrations/ --context {Module}DbContext`

- Application

Navigate terminal to Shared.Infrastructure and run the following.

`dotnet ef migrations add "initial" --startup-project ../../API -o Persistence/Migrations/ --context ApplicationDbContext`

- Identity

Navigate terminal to Modules.Identity.Infrastructure and run the following.

`dotnet ef migrations add "initial" --startup-project ../../../API -o Persistence/Migrations/ --context IdentityDbContext`

- Catalog

Navigate terminal to Modules.Catalog.Infrastructure and run the following.

`dotnet ef migrations add "initial" --startup-project ../../../API -o Persistence/Migrations/ --context CatalogDbContext`