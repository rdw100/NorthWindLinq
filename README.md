# NorthWindLinq
This LINQ Query demonstration project was created using using Visual Studio 2019 16.5.4, .NET Core 3.1.201, EF Core 3.1.3, & SQL Server 13.0.4001.  The objective is to demonstrate LINQ-to-Entity queries using the Northwind database.

## Instructions
Instructions to run this demo project are as follows:
1. Open Project
   Download NorthWindLinq project and open it in Visual Studio 2019.

2. Create Database
   Follow run and execute Northwind script instructions from the [NorthWind-Pubs Repo](https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs).  This project created a Northwind database in the default local database server instance mssqllocaldb.

3. Verify Connection 
  Verify the connection string in NorthwindContext in Visual Studio and make sure that it is pointing to your local DB server.  For       example, this project uses the following: 

     ```   Server=(localdb)\\mssqllocaldb;Database=Northwind;Trusted_Connection=True;MultipleActiveResultSets=true```

## Package List
The package list for this project is as follows:
> list project nuget packages
```shell
PM> dotnet list package
```
```
Project 'NorthWindLinq' has the following package references
   netcoreapp3.1:
   Top-level Package                              Requested   Resolved
   > consoletables                                2.4.1       2.4.1   
   > Microsoft.EntityFrameworkCore                3.1.3       3.1.3   
   > Microsoft.EntityFrameworkCore.SqlServer      3.1.3       3.1.3   
   > Microsoft.EntityFrameworkCore.Tools          3.1.3       3.1.3
```
## Setup
This project was created using the following development setup with NuGet Package Manager in VS 2019.  The steps to setup this project were as follows. 

> update and install these packages first to execute EF scaffold
```shell
PM> Install-Package Microsoft.EntityFrameworkCore
PM> Install-Package Microsoft.EntityFrameworkCore.Tools
PM> Install-Package Microsoft.EntityFrameworkCore.SqlServer
```
> execute scaffold to create create model from an existing Northwind database using Entity Framework Core 
```shell
PM> Scaffold-DbContext "Server=(localdb)\mssqllocaldb;Database=Northwind;Trusted_Connection=True;MultipleActiveResultSets=true" Microsoft.EntityFrameworkCore.SqlServer -Tables Employees,Shippers,Customers,Suppliers,Categories,Products,Orders,"Order Details" -OutputDir Models
```
> format list output using console tables  
```shell
PM> Install-Package ConsoleTables
```

**Execution**
> open the developer command prompt from within the opened solution in VS 2019 via Tools > Command Line > Developer Command Prompt
```shell
> dotnet --version
> dotnet run -p NorthWindLinq
```
![Recordit GIF](https://github.com/rdw100/NorthWindLinq/blob/master/NorthWindLinq/img/oHI9IGxRnt.gif?raw=true)

## Output
Typed/untyped object lists are printed to the console inside a formatted table using ConsoleTables.

 | CustomerId | OrderId | ProductName                     | UnitPrice | Quantity | OrderDate  |
 | ---------- | ------- | ------------------------------- | --------- | -------- | :--------: |
 | FRANK      | 10267   | Boston Crab Meat                | 14.7000   | 50       | 1996-07-29 |
