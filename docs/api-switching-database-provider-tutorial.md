# Documentation

### Switching Database Providers

fluentpos currently supports MSSQL & Postgres Dbs. Due to issues with schemas in EFCore's MySQL implementation, MySQL is not being used here.

Firstly, you need to make sure that valid connection strings are mentioned in the appSetting.json
Next, set either to true in appSetting under `PersistenceSettings`.

`"UseMsSql": true,`

`"UsePostgres": true,`

Note: If you set both to true, Postgres will be used by default.

### Important 
- While Switching DBProviders via EFcore, make sure to delete all the migrations, and re-add migrations via the below CLI Command.
- Make sure that you drop the existing database if any.

## Steps 
- Navigate to each of the Infrastructure project per module and shared(Shared.Infrastructure)
- Open the directory in terminal mode. PFB the attached screenshot. You just have to right the Infrastructure project in Visual Studio and select `Open in Terminal`.

![image](https://user-images.githubusercontent.com/31455818/122291148-1d211380-cf12-11eb-9f28-35e5ec0989e5.png)
- Run the EF commands. You can find the EF Commands below in the next section with additional steps ;)

![image](https://user-images.githubusercontent.com/31455818/122291196-2d38f300-cf12-11eb-965e-27267fd1fc76.png)
- That's it!



### Application

Navigate terminal to Shared.Infrastructure and run the following.

`dotnet ef migrations add "initial" --startup-project ../../API -o Persistence/Migrations/ --context ApplicationDbContext`

### Identity

Navigate terminal to Modules.Identity.Infrastructure and run the following.

`dotnet ef migrations add "initial" --startup-project ../../../API -o Persistence/Migrations/ --context IdentityDbContext`

### Catalog

Navigate terminal to Modules.Catalog.Infrastructure and run the following.

`dotnet ef migrations add "initial" --startup-project ../../../API -o Persistence/Migrations/ --context CatalogDbContext`

### People

Navigate terminal to Modules.People.Infrastructure and run the following.

`dotnet ef migrations add "initial" --startup-project ../../../API -o Persistence/Migrations/ --context PeopleDbContext`
