# AdressBook_WebAPI_CORE
Project for Learning Web.API with .NET CORE  (Please read the wiki tag)

If you want to download the project and try to connect the database.

1. Download the script for SQL-Server - "adressbook_createtable.sql"
2. Click to open it in SQL Server
3. Check if the FILENAME has got the right path in row 7 and 9 and if you got the right version of SQL Server otherwise try to change it.
( NAME = N'AdressBook', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\AdressBook.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AdressBook_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\AdressBook_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
4. Execute the sqript
5. If there is somthing wrong in the connection to the database check
The connectionstring is in the file appsettings.json  maybe you have to change the DataSource to your instance of SQL-Server
./SQLEXPRESS if you are using the Microsoft SQL Express version.

  "ConnectionStrings": {
    "DefaultConnection": "Data Source=.;Initial Catalog=AdressBook;Integrated Security=True"
  }
