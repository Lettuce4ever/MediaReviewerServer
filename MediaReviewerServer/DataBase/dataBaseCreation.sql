﻿Use master
Go
IF EXISTS (SELECT * FROM sys.databases WHERE name = N'MediaReviewerDB')
BEGIN
    DROP DATABASE MediaReviewerDB;
END
Go
Create Database MediaReviewerDB
Go
Use MediaReviewerDB
Go

Create Table Users
(
UserID int Primary Key Identity,
Username nvarchar(50) Not Null,
Password nvarchar(50) Not Null,
Firstname nvarchar(256) Not Null,
Lastname nvarchar(256) Not Null,
Email nvarchar(50) Unique Not Null,
IsAdmin bit Not Null,
)

Create Table Genres
(
GenreID int Primary Key Identity, 
GenreName nvarchar(256) Unique Not Null
)

Create Table Movies
(
MovieID int Primary Key Identity,
MovieName nvarchar(256) Not Null,
ReleaseYear int Not Null,
Length int Not Null,
Description nvarchar(1024) Not Null,
Rating Float Not Null,
Image nvarchar(Max) Not Null,
Trailer nvarchar(Max) Not Null,
Director nvarchar(256) Not Null,
Star nvarchar(256) Not Null,
Writer nvarchar(256) Not Null,
MultiDirectors bit Not Null,
MultiStars bit Not Null,
MultiWriters bit Not Null
)


Create Table Reviews
(
ReviewID int Primary Key Identity,
UserID int Foreign Key References Users(UserID),
MovieID int Foreign Key References Movies(MovieID),
Rating Float Not Null,
Description nvarchar(1024) Not Null,
ReviewDate Date Not Null
)

Create Table Requests
(
RequestID int Primary Key Identity,
UserID int Foreign Key References Users(UserID),
Title nvarchar(256) Not Null,
Description nvarchar(1024) Not Null,
RequestDate Date Not Null
)

Create Table GenresToMovies
(
MovieID int Not Null Foreign Key References Movies(MovieID),
GenreID int Not Null Foreign Key References Genres(GenreID) 
Primary Key (MovieID, GenreID)
)

-- Create a login for the admin user
CREATE LOGIN [MediaReviewerAdminLogin] WITH PASSWORD = 'pass031206';
Go

-- Create a user in the TasksManagementDB database for the login
CREATE USER [MediaReviewerUser] FOR LOGIN [MediaReviewerAdminLogin];
Go

-- Add the user to the db_owner role to grant admin privileges
ALTER ROLE db_owner ADD MEMBER [MediaReviewerAdminUser];
Go
--so user can restore the DB!
ALTER SERVER ROLE sysadmin ADD MEMBER [MediaReviewerAdminLogin];
Go

--EF Code
/*
scaffold-DbContext "Server = (localdb)\MSSQLLocalDB;Initial Catalog=MediaReviewerDB;User ID=MediaReviewerAdminLogin;Password=pass031206;" Microsoft.EntityFrameworkCore.SqlServer -OutPutDir Models -Context MediaReviewerDbContext -DataAnnotations -force
*/


INSERT INTO Users(Username, Password, Firstname, Lastname, Email, IsAdmin) Values('TheGal', 'Gal031206', 'Gal', 'Klug', 'galkluger@gmail.com', 1)
INSERT INTO Users(Username, Password, Firstname, Lastname, Email, IsAdmin) Values('TheShahar', 'Shahar031206', 'Shahar', 'Klug', 'shaharkluger@gmail.com', 0)
INSERT INTO Users(Username, Password, Firstname, Lastname, Email, IsAdmin) Values('TheRan', 'Ran031206', 'Ran', 'Klug', 'rankluger@gmail.com', 0)
INSERT INTO Users(Username, Password, Firstname, Lastname, Email, IsAdmin) Values('TheRan_AFTERBU', 'Ran1111031206', 'Ran', 'Klug', 'rankluger1111@gmail.com', 0)
Select*from Users