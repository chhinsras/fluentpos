<p align="center">
  <a href="https://github.com/fluentpos/fluentpos">
    <img src="https://codewithmukesh.com/wp-content/uploads/2021/06/fluentposBanner.png" alt="fluentpos">
  </a>
  <h3 align="center">fluentpos - Open Source Point Of Sales / Inventory Management Solution</h3>
  <p align="center">
    Built with ASP.NET Core 5.0 WebAPI & Angular 12 Material
  </p>
</p>

### About fluentpos


Having quite a lot of experience with POS & Inventory Management system, we set out to build out a full fledged open source system using our favorite tech stack and tools. Modular development was a prime requirement for us when we got started. Adapting to a Microservice architecture was the first choice we had. But given the complexities with the mentioned architecture, we decided to stay away from it atleast for the starting. 

There actually was no real need to implement microservices. fluentpos was meant to help businesses in their day-to-day activities. For this, a well designed monolith application would also do the trick. We were clear to have the API and UI seperated, to give oppurtunities to multiple client apps in the future.

For API, ASP.NET Core 5.0 was our obvious choice. As for the UI, we decided to go with Angular 12 Material UI.

The WebAPI application had to be highly modular to improve development experience. This needed breaking down the application to logical modules like Identity, Catalog, Sales, Inventory. Each of these modules has its own controllers / interfaces / dbContext. As for the DB providers, postgres / mssql will be used. One module cannot directly talk to the other module nor modify its table. CrossCutting concerns would use interfaces/ events. And yes, domain events are also included in the project using mediatr Handler. Each of the module follows a clean architecture design / Onion / Hex.

fluentpos was meant for retail businesses. The modular monolith architecture would help us to extend fluentpos to support other business modules like cafe, restaurant, warehouses and so.


### Technology Stack :muscle:

- API - ASP.NET Core 5.0 WebAPI
- Client - Angular 12 Material
- Data Access - [Entity Framework Core 5.0](https://docs.microsoft.com/en-us/ef/core/)
- DB Providers - Postgres, MSSQL

### Project Status

- API - `In Progress`
- Angular Project - `Not Yet Started`

### Getting Started

> fluentpos is in it's early development stage.

### The Team
- Mukesh Murugan [@iammukeshm](https://github.com/iammukeshm/)
- Chhin Sras [@chhinsras](https://github.com/chhinsras)

### Community
- Discord [@fluentpos](https://discord.gg/PAErG25QPK)
