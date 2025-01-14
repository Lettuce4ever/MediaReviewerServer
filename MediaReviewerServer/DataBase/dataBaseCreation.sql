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
Username nvarchar(16) Not Null,
Password nvarchar(16) Not Null,
Firstname nvarchar(16) Not Null,
Lastname nvarchar(16) Not Null,
Email nvarchar(256) Not Null,
IsAdmin bool Not Null,
Image nvarchar
)

Create Table Genres
(
GenreID int Primary Key Identity, 
GenreName nvarchar(16)
)

Create Table Contents
(
ContentID int Primary Key Identity,
ContentName nvarchar(256) Not Null,
ReleaseYear int Not Null,
Length int Not Null,
Description nvarchar(1024) Not Null,
Rating Float Not Null,
Image nvarchar Not Null,
)

Create Table Movies
(
ContentID int Primary Key Foreign Key References Contents(ContentID),
Trailer nvarchar Not Null,
Director nvarchar(256) Not Null,
Star nvarchar(256) Not Null,
Writer nvarchar(256) Not Null,
MultiDirectors bool Not Null,
MultiStars bool Not Null,
MultiWriters bool Not Null,
)

Create Table Series
(
ContentID int Primary Key Foreign Key References Contents(ContentID),
Trailer nvarchar Not Null,
Creator nvarchar(256) Not Null,
Star nvarchar(256) Not Null,
Writer nvarchar(256) Not Null,
MultiCreators bool Not Null,
MultiStars bool Not Null,
MultiWriters bool Not Null,
)

Create Table Books
(
ContentID int Primary Key Foreign Key References Contents(ContentID),
Author nvarchar(256) Not Null,
MultiAuthors bool Not Null,
)

Create Table Reviews
(
ReviewID int Primary Key Identity,
UserID int Foreign Key References Users(UserID),
ContentID int Foreign Key References Contents(ContentID),
Rating Float Not Null,
Description nvarchar(1024) Not Null,
Date Nvarchar(16) Not Null
)

Create Table Requests
(
RequestID int Primary Key Identity,
UserID int Foreign Key References Users(UserID),
Title nvarchar(256) Not Null,
Description nvarchar(1024) Not Null,
Date nvarchar(16) Not Null
)

Create Table GenresToContents
(
ContentID int Foreign Key References Contents(ContentID),
GenreID int Foreign Key References Genres(GenreID)
)