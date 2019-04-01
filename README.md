# AdressBook_WebAPI_CORE
Project for Learning Web.API with .NET CORE  (Please read the wiki tag)

If you want to download the project and try to connect the database.

1. Download the script for SQL-Server
2. Click to open it in SQL Server
3. Check if the FILENAME has got the right path in row 7 and 9 and if you got the right version of SQL Server otherwise try to change it.
( NAME = N'AdressBook', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\AdressBook.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AdressBook_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\AdressBook_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
4. Execute the sqript
