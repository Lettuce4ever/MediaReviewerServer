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
Email nvarchar(50) Not Null,
IsAdmin bit Not Null,
Image nvarchar(Max)
)

Create Table Genres
(
GenreID int Primary Key Identity, 
GenreName nvarchar(256) Not Null
)

Create Table Contents
(
ContentID int Primary Key Identity,
ContentName nvarchar(256) Not Null,
ReleaseYear int Not Null,
Length int Not Null,
Description nvarchar(1024) Not Null,
Rating Float Not Null,
Image nvarchar(Max) Not Null
)

Create Table Movies
(
ContentID int Primary Key Foreign Key References Contents(ContentID),
Trailer nvarchar(Max) Not Null,
Director nvarchar(256) Not Null,
Star nvarchar(256) Not Null,
Writer nvarchar(256) Not Null,
MultiDirectors bit Not Null,
MultiStars bit Not Null,
MultiWriters bit Not Null
)

Create Table Series
(
ContentID int Primary Key Foreign Key References Contents(ContentID),
Trailer nvarchar(Max) Not Null,
Creator nvarchar(256) Not Null,
Star nvarchar(256) Not Null,
Writer nvarchar(256) Not Null,
MultiCreators bit Not Null,
MultiStars bit Not Null,
MultiWriters bit Not Null
)

Create Table Books
(
ContentID int Primary Key Foreign Key References Contents(ContentID),
Author nvarchar(256) Not Null,
MultiAuthors bit Not Null
)

Create Table Reviews
(
ReviewID int Primary Key Identity,
UserID int Foreign Key References Users(UserID),
ContentID int Foreign Key References Contents(ContentID),
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

Create Table GenresToContents
(
ContentID int Not Null Foreign Key References Contents(ContentID),
GenreID int Not Null Foreign Key References Genres(GenreID)
Primary Key (ContentID, GenreID)
)