# AgileResultsMVC

Application for your task list based Agile Results approaches develops based ASP NET Core MVC and Entity Framework. 

# How to install

This project uses SQL Server LocalDB. 

To create database need (using NET Core CLI):

* Add initial migration:
 ```sh
 dotnet ef migrations add migrationName
 ```
 * Update database
  ```sh
  dotnet ef database update
  ```
* Run!