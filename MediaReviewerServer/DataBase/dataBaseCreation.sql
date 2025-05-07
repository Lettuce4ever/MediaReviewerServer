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

INSERT INTO Genres(GenreName) Values('Superhero')
INSERT INTO Genres(GenreName) Values('Coming-of-Age')
INSERT INTO Genres(GenreName) Values('Slasher Horror')
INSERT INTO Genres(GenreName) Values('Horror')
INSERT INTO Genres(GenreName) Values('Raunchy Comedy')
INSERT INTO Genres(GenreName) Values('Comedy')
INSERT INTO Genres(GenreName) Values('Dark Comedy')
INSERT INTO Genres(GenreName) Values('Thriller')
INSERT INTO Genres(GenreName) Values('Steamy Romance')
INSERT INTO Genres(GenreName) Values('Romance')
INSERT INTO Genres(GenreName) Values('Anime')
INSERT INTO Genres(GenreName) Values('Dystopian Sci-Fi')
INSERT INTO Genres(GenreName) Values('Sci-Fi')
INSERT INTO Genres(GenreName) Values('Sitcom')
INSERT INTO Genres(GenreName) Values('Gangster')
INSERT INTO Genres(GenreName) Values('Fantasy Epic')
INSERT INTO Genres(GenreName) Values('Fantasy')
INSERT INTO Genres(GenreName) Values('Psychological Thriller')
INSERT INTO Genres(GenreName) Values('Crime')
INSERT INTO Genres(GenreName) Values('Biography')
INSERT INTO Genres(GenreName) Values('Drama')
INSERT INTO Genres(GenreName) Values('Adventure')
INSERT INTO Genres(GenreName) Values('War')
INSERT INTO Genres(GenreName) Values('One-Person Army Action')
INSERT INTO Genres(GenreName) Values('Action')
INSERT INTO Genres(GenreName) Values('Erotic Thriller')
INSERT INTO Genres(GenreName) Values('Psychological Drama')
INSERT INTO Genres(GenreName) Values('Mystery')
INSERT INTO Genres(GenreName) Values('Suspense Mystery')
INSERT INTO Genres(GenreName) Values('Supernatural Horror')
INSERT INTO Genres(GenreName) Values('Psychological Horror')
INSERT INTO Genres(GenreName) Values('Documentary')
INSERT INTO Genres(GenreName) Values('Animation')
INSERT INTO Genres(GenreName) Values('Dark Fantasy')
INSERT INTO Genres(GenreName) Values('Romantic Comedy')
INSERT INTO Genres(GenreName) Values('Serial Killer')
INSERT INTO Genres(GenreName) Values('Sci-Fi Epic')
INSERT INTO Genres(GenreName) Values('Epic')
INSERT INTO Genres(GenreName) Values('Tragedy')
INSERT INTO Genres(GenreName) Values('Drug Crime')
INSERT INTO Genres(GenreName) Values('Computer Animation')
INSERT INTO Genres(GenreName) Values('Supernatural Fantasy')
INSERT INTO Genres(GenreName) Values('Teen Adventure')
INSERT INTO Genres(GenreName) Values('Teen Comedy')
INSERT INTO Genres(GenreName) Values('Teen Epic')
INSERT INTO Genres(GenreName) Values('Action Epic')
INSERT INTO Genres(GenreName) Values('Docudrama')
INSERT INTO Genres(GenreName) Values('Historical Epic')
INSERT INTO Genres(GenreName) Values('Period Drama')
INSERT INTO Genres(GenreName) Values('Prison Drama')

INSERT INTO Movies(MovieName, ReleaseYear, Length, Description, Rating, Image, Trailer, Director, Star, Writer, MultiDirectors, MultiStars, MultiWriters) Values('Iron Man', 2008, 126, 'After being held captive in an Afghan cave, billionaire engineer Tony Stark creates a unique weaponized suit of armor to fight evil.', -1, 'https://m.media-amazon.com/images/M/MV5BMTczNTI2ODUwOF5BMl5BanBnXkFtZTcwMTU0NTIzMw@@._V1_FMjpg_UX1000_.jpg', 'https://youtu.be/8ugaeA-nMTc?si=95Mfetqrv87fxv7Q', 'Jon Favreau', 'Robert Downey Jr., Gwyneth Paltrow, Terrence Howard', 'Mark Fergus, Hawk Ostby, Art Marcum', 0, 1, 1)
--Sci-Fi Epic
INSERT INTO GenresToMovies(MovieID, GenreID) Values(1,37) 
--Superhero
INSERT INTO GenresToMovies(MovieID, GenreID) Values(1,1)
--Action
INSERT INTO GenresToMovies(MovieID, GenreID) Values(1,25)
--Adventure
INSERT INTO GenresToMovies(MovieID, GenreID) Values(1,22)
--Sci-Fi
INSERT INTO GenresToMovies(MovieID, GenreID) Values(1,13)
INSERT INTO Movies(MovieName, ReleaseYear, Length, Description, Rating, Image, Trailer, Director, Star, Writer, MultiDirectors, MultiStars, MultiWriters) Values('The Godfather', 1972, 175, 'The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.', -1, 'https://m.media-amazon.com/images/M/MV5BNGEwYjgwOGQtYjg5ZS00Njc1LTk2ZGEtM2QwZWQ2NjdhZTE5XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg', 'https://youtu.be/w3Wo6QiD3eU?si=uzCy5stqF3W9jVts', 'Francis Ford Coppola', 'Marlon Brando, Al Pacino, James Caan', 'Mario Puzo, Francis Ford Coppola', 0, 1, 1)
--Epic
INSERT INTO GenresToMovies(MovieID, GenreID) Values(2,38)
--Gangster
INSERT INTO GenresToMovies(MovieID, GenreID) Values(2,15)
--Tragedy
INSERT INTO GenresToMovies(MovieID, GenreID) Values(2,39)
--Crime
INSERT INTO GenresToMovies(MovieID, GenreID) Values(2,19)
--Drama
INSERT INTO GenresToMovies(MovieID, GenreID) Values(2,21)
INSERT INTO Movies(MovieName, ReleaseYear, Length, Description, Rating, Image, Trailer, Director, Star, Writer, MultiDirectors, MultiStars, MultiWriters) Values('Pulp Fiction', 1994, 154, 'The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.', -1, 'https://m.media-amazon.com/images/M/MV5BYTViYTE3ZGQtNDBlMC00ZTAyLTkyODMtZGRiZDg0MjA2YThkXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg', 'https://youtu.be/s7EdQ4FqbhY?si=oFUZl1vG1Kcad1h2', 'Quentin Tarantino', 'John Travolta, Uma Thurman, Samuel L. Jackson', 'Quentin Tarantino, Roger Avary', 0, 1, 1)
--Dark Comedy
INSERT INTO GenresToMovies(MovieID, GenreID) Values(3,7)
--Drug Crime
INSERT INTO GenresToMovies(MovieID, GenreID) Values(3,40)
--Gangster
INSERT INTO GenresToMovies(MovieID, GenreID) Values(3,15)
--Crime
INSERT INTO GenresToMovies(MovieID, GenreID) Values(3,19)
--Drama
INSERT INTO GenresToMovies(MovieID, GenreID) Values(3,21)
INSERT INTO Movies(MovieName, ReleaseYear, Length, Description, Rating, Image, Trailer, Director, Star, Writer, MultiDirectors, MultiStars, MultiWriters) Values('Spider-Man: Into the Spider-Verse', 2018, 117, 'Teen Miles Morales becomes the Spider-Man of his universe and must join with five spider-powered individuals from other dimensions to stop a threat for all realities.', -1, 'https://m.media-amazon.com/images/M/MV5BMjMwNDkxMTgzOF5BMl5BanBnXkFtZTgwNTkwNTQ3NjM@._V1_FMjpg_UX1000_.jpg', 'https://youtu.be/g4Hbz2jLxvQ?si=VXhc76ASqLjw19TD', 'Bob Persichetti, Peter Ramsey, Rodney Rothman', 'Shameik Moore, Jake Johnson, Hailee Steinfeld', 'Phil Lord, Rodney Rothman', 1, 1, 1)
--Computer Animation
INSERT INTO GenresToMovies(MovieID, GenreID) Values(4,41)
--Superhero
INSERT INTO GenresToMovies(MovieID, GenreID) Values(4,1)
--Supernatural Fantasy
INSERT INTO GenresToMovies(MovieID, GenreID) Values(4,42)
--Teen Adventure
INSERT INTO GenresToMovies(MovieID, GenreID) Values(4,43)
--Teen Comedy
INSERT INTO GenresToMovies(MovieID, GenreID) Values(4,44)
INSERT INTO Movies(MovieName, ReleaseYear, Length, Description, Rating, Image, Trailer, Director, Star, Writer, MultiDirectors, MultiStars, MultiWriters) Values('The Batman', 2022, 176, 'When a sadistic serial killer begins murdering key political figures in Gotham, the Batman is forced to investigate the city''s hidden corruption and question his family''s involvement.', -1, 'https://m.media-amazon.com/images/M/MV5BMmU5NGJlMzAtMGNmOC00YjJjLTgyMzUtNjAyYmE4Njg5YWMyXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg', 'https://youtu.be/mqqft2x_Aa4?si=0c2fcENlkROEy09v', 'Jon Favreau', 'Robert Pattinson, Zoë Kravitz, Jeffrey Wright', 'Mark Fergus, Hawk Ostby, Art Marcum', 0, 1, 1)
--Action Epic
INSERT INTO GenresToMovies(MovieID, GenreID) Values(5,46)
--Epic
INSERT INTO GenresToMovies(MovieID, GenreID) Values(5,38)
--Serial Killer
INSERT INTO GenresToMovies(MovieID, GenreID) Values(5,36)
--Superhero
INSERT INTO GenresToMovies(MovieID, GenreID) Values(5,1)
--Action
INSERT INTO GenresToMovies(MovieID, GenreID) Values(5,25)
INSERT INTO Movies(MovieName, ReleaseYear, Length, Description, Rating, Image, Trailer, Director, Star, Writer, MultiDirectors, MultiStars, MultiWriters) Values(' Schindler''s List', 1993, 195, 'In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.', -1, 'https://m.media-amazon.com/images/M/MV5BNjM1ZDQxYWUtMzQyZS00MTE1LWJmZGYtNGUyNTdlYjM3ZmVmXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg', 'https://youtu.be/gG22XNhtnoY?si=QHkIZHY257CwQ8BE', 'Steven Spielberg', 'Liam Neeson, Ralph Fiennes, Ben Kingsley', 'Thomas Keneally, Steven Zaillian', 0, 1, 1)
--Docudrama
INSERT INTO GenresToMovies(MovieID, GenreID) Values(6,47)
--Epic
INSERT INTO GenresToMovies(MovieID, GenreID) Values(6,38)
--Historical Epic
INSERT INTO GenresToMovies(MovieID, GenreID) Values(6,48)
--Period Drama
INSERT INTO GenresToMovies(MovieID, GenreID) Values(6,49)
--Prison Drama
INSERT INTO GenresToMovies(MovieID, GenreID) Values(6,50)
Select*from Users
Select*from Genres
Select*from Genres ORDER BY GenreID ASC
Select*from Movies
Select*from GenresToMovies
