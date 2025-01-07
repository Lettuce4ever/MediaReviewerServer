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
Username nvarchar(16) Primary Key,
Password nvarchar(16) Not Null,
Firstname nvarchar(16) Not Null,
Lastname nvarchar(16) Not Null,
Email nvarchar(256) Not Null,
IsAdmin bool Not Null
)

Create Table ContentTypes
(
TypeID int Primary Key Identity,
TypeName nvarchar(16) Not Null
)

Create Table Genres
(
Genre nvarchar(16) Primary Key
)

Create Table Contents
(
ContentID int Primary Key Identity,
ContentName nvarchar(256) Not Null,
ReleaseYear int Not Null,
Length int Not Null,
Description nvarchar(1024) Not Null,
Rating Float,
Image nvarchar(256) Not Null,
Trailer nvarchar(256),
Author nvarchar(256),
Creator nvarchar(256),
Director nvarchar(256),
Star nvarchar(256),
Writer nvarchar(256),
MultiAuthors bool,
MultiCreators bool,
MultiDirectors bool,
MultiStars bool,
MultiWriters bool,
)

Create Table Reviews
(
ReviewID int Primary Key Identity,
Username nvarchar Foreign Key References Users(Username),
ContentID int Foreign Key References Contents(ContentID),
Rating Float Not Null,
Description nvarchar(1024) Not Null,
Date Nvarchar(16) Not Null
)

Create Table Requests
(
RequestID int Primary Key Identity,
Username nvarchar Foreign Key References Users(Username),
Title nvarchar(256) Not Null,
Description nvarchar(1024) Not Null,
Date nvarchar(16) Not Null
)

Create Table GenresToContents
(
ContentID int Foreign Key References Contents(ContentID) Identity,
Genre nvarchar Foreign Key References Genres(Genre)
)