<p align="center">
  <a href="https://github.com/fluentpos/fluentpos">
    <img src="https://codewithmukesh.com/wp-content/uploads/2021/06/fluentposBanner.png" alt="fluentpos">
  </a>
  <h3 align="center">fluentpos - Open Source Point Of Sales / Inventory Management Solution</h3>
  <p align="center">
    Built with ASP.NET Core 5.0 WebAPI & Angular 12 Material UI
  </p>
</p>

### About fluentpos

Having quite a lot of experience with POS & Inventory Management system, we set out to build out a full fledged open source system using our favorite tech stack and tools. Modular development was a prime requirement for us when we got started. Adapting to a Microservice architecture was the first choice we had. But given the complexities with the mentioned architecture, we decided to stay away from it atleast for the starting. 

There actually was no real need to implement microservices. fluentpos was meant to help businesses in their day-to-day activities. For this, a well designed monolith application would also do the trick. We were clear to have the API and UI seperated, to give oppurtunities to multiple client apps in the future.

For API, ASP.NET Core 5.0 was our obvious choice. As for the UI, we decided to go with Angular 12 Material UI.

The WebAPI application had to be highly modular to improve development experience. This needed breaking down the application to logical modules like Identity, Catalog, Sales, Inventory. Each of these modules has its own controllers / interfaces / dbContext. As for the DB providers, postgres / mssql will be used. One module cannot directly talk to the other module nor modify its table. CrossCutting concerns would use interfaces/ events. And yes, domain events are also included in the project using mediatr Handler. Each of the module follows a clean architecture design / Onion / Hex.

fluentpos was meant for retail businesses. The modular monolith architecture would help us to extend fluentpos to support other business modules like cafe, restaurant, warehouses and so.

### Roadmap

For the initial release, you can track the progress and included tasks here - https://github.com/fluentpos/fluentpos/milestone/1

PS, we will be adding in more requirements.

### Note to contributors

- Identify / Create tasks from / to https://github.com/fluentpos/fluentpos/issues
- Assign it to yourself
- Create a branch from the master branch and name it according to the Issue Id. For ex, if the Issue id is #70, create a branch 'fluentpos-70'
- Make your changes in this branch
- Get it reviewed from one of the Core Team Members
- Submit a PR
- Get it Merged to the Master Branch

### Technology Stack :muscle:

- API - ASP.NET Core 5.0 WebAPI
- Client - Angular 12 Material
- Data Access - [Entity Framework Core 5.0](https://docs.microsoft.com/en-us/ef/core/)
- DB Providers - Postgres, MSSQL

### Project Status

- API - `In Progress`
- Angular Project - `In Progress`
- Seperate Website to maintain documentation - `Coming Soon!`
- Docker Container - `Coming Soon!`

### Getting Started

> fluentpos is in it's early development stage.

Clone this repository to your local machine.

#### Prerequisites to run API

1. Install the latest [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0)
2. Install the latest DOTNET & EF CLI Tools by using this command `dotnet tool install --global dotnet-ef` 
3. Install the latest version of Visual Studio IDE 2019 (v16.8 and above) OR Visual Studio Code 🚀
4. It's recommended to use Postgres DB as it comes by default with fluentpos. Install [PostgreSQL](https://www.postgresql.org/download/). 
5. As for quick DB Management, we love [Azure Data Studio](https://docs.microsoft.com/en-us/sql/azure-data-studio/download-azure-data-studio?view=sql-server-ver15)

#### Running the API

1. Open up `FluentPOS.sln` in Visual Studio 2019.
2. Navigate to appSettings.json under `src/Api/Bootstrapper/appsettings.json`
3. Add you PostgreSQL connection string under `PersistenceSettings`. The default connection string is `"postgres": "Host=localhost;Database=fluentpos;Username=postgres;Password=root"`
4. That's everything you need to setup the API. Just build and run the API project.
5. By default, the database is migrated and latest changes are applied.
6. Some default data is also seeded to this database like roles, users, brands, products etc.

> Not interested with PostgreSQL? You can easily switch to MSSQL by following this [guide]( https://github.com/fluentpos/fluentpos/blob/master/docs/api-switching-database-provider-tutorial.md).

#### Running Angular

- Navigate to fluentpos\src\client via terminal.
- Run `npm install` to install all the required packages
- Run `ng serve`
- Navigate to localhost:4200 on your browser

#### Visual Studio Code
1. Open up `fluentpos.code-workspace` inside fluentpos\workspace
2. Make sure you have Visual Studio Code installed as well as .NET and Angular related extensions
3. Open terminal session
4. Running API with CLI command: `dotnet watch run`

https://user-images.githubusercontent.com/31455818/126076884-47989b52-368e-4714-b90b-093779a7607f.mp4

POS UI - As of 25/7/2021

https://user-images.githubusercontent.com/31455818/126880716-67545770-e74c-4e20-b42f-8c247e91f834.mp4





#### Default Credentials

- superadmin - superadmin@fluentpos.com / 123Pa$$word!
- staff - staff@fluentpos.com / 123Pa$$word!

You can use these credentials to generate jwt tokens in the `api/identity/tokens` endpoint.




### Note

Since fluentpos is in it's early development stage, I have not been able to write detailed documentation about the implementation. You can expect quite a lot of content around this architecture on my blog [@codewithmukesh](https://codewithmukesh.com/) in the upcoming days.

### The Team

- Mukesh Murugan [@iammukeshm](https://github.com/iammukeshm/)
- Chhin Sras [@chhinsras](https://github.com/chhinsras)
- Nikolay Chebotov [@unchase](https://github.com/unchase)

### Community

- Discord [@fluentpos](https://discord.gg/PAErG25QPK)

## License

This project is licensed with the [MIT license](LICENSE).
