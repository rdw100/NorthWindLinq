# NorthWindLinq
### Setup

- This project was created using the following development setup with NuGet Package Manager in VS 2019.

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
> open the developer command prompt with open solution in VS 2019 (Tools > Command Line > Developer Command Prompt)
![Recordit GIF](https://github.com/rdw100/NorthWindLinq/blob/master/NorthWindLinq/img/oHI9IGxRnt.gif?raw=true)
```shell
> dotnet --version
> dotnet run -p NorthWindLinq
```
