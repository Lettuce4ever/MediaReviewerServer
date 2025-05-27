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
INSERT INTO Users(Username, Password, Firstname, Lastname, Email, IsAdmin) Values('TheTami', 'Tami031206', 'Tami', 'Klug', 'tamikluger@gmail.com', 0)

INSERT INTO Genres(GenreName) Values('Superhero') --1
INSERT INTO Genres(GenreName) Values('Coming-of-Age') --2
INSERT INTO Genres(GenreName) Values('Slasher Horror') --3
INSERT INTO Genres(GenreName) Values('Horror') --4
INSERT INTO Genres(GenreName) Values('Raunchy Comedy') --5
INSERT INTO Genres(GenreName) Values('Comedy') --6
INSERT INTO Genres(GenreName) Values('Dark Comedy') --7
INSERT INTO Genres(GenreName) Values('Thriller') --8
INSERT INTO Genres(GenreName) Values('Steamy Romance') --9
INSERT INTO Genres(GenreName) Values('Romance') --10
INSERT INTO Genres(GenreName) Values('Anime') --11
INSERT INTO Genres(GenreName) Values('Dystopian Sci-Fi') --12
INSERT INTO Genres(GenreName) Values('Sci-Fi') -- 13
INSERT INTO Genres(GenreName) Values('Sitcom') -- 14
INSERT INTO Genres(GenreName) Values('Gangster') --15
INSERT INTO Genres(GenreName) Values('Fantasy Epic') --16
INSERT INTO Genres(GenreName) Values('Fantasy') --17
INSERT INTO Genres(GenreName) Values('Psychological Thriller') --18
INSERT INTO Genres(GenreName) Values('Crime') --19
INSERT INTO Genres(GenreName) Values('Biography') --20
INSERT INTO Genres(GenreName) Values('Drama') --21
INSERT INTO Genres(GenreName) Values('Adventure') --22
INSERT INTO Genres(GenreName) Values('War') --23
INSERT INTO Genres(GenreName) Values('One-Person Army Action') --24
INSERT INTO Genres(GenreName) Values('Action') --25
INSERT INTO Genres(GenreName) Values('Erotic Thriller') --26
INSERT INTO Genres(GenreName) Values('Psychological Drama') --27
INSERT INTO Genres(GenreName) Values('Mystery') --28
INSERT INTO Genres(GenreName) Values('Suspense Mystery') --29
INSERT INTO Genres(GenreName) Values('Supernatural Horror') --30
INSERT INTO Genres(GenreName) Values('Psychological Horror') --31
INSERT INTO Genres(GenreName) Values('Documentary') --32
INSERT INTO Genres(GenreName) Values('Animation') --33
INSERT INTO Genres(GenreName) Values('Dark Fantasy') --34
INSERT INTO Genres(GenreName) Values('Romantic Comedy') --35
INSERT INTO Genres(GenreName) Values('Serial Killer') --36
INSERT INTO Genres(GenreName) Values('Sci-Fi Epic') --37
INSERT INTO Genres(GenreName) Values('Epic') --38
INSERT INTO Genres(GenreName) Values('Tragedy') --39
INSERT INTO Genres(GenreName) Values('Drug Crime') --40
INSERT INTO Genres(GenreName) Values('Computer Animation') --41
INSERT INTO Genres(GenreName) Values('Supernatural Fantasy') --42
INSERT INTO Genres(GenreName) Values('Teen Adventure') --43
INSERT INTO Genres(GenreName) Values('Teen Comedy') --44
INSERT INTO Genres(GenreName) Values('Teen Epic') --45
INSERT INTO Genres(GenreName) Values('Action Epic') --46
INSERT INTO Genres(GenreName) Values('Docudrama') --47
INSERT INTO Genres(GenreName) Values('Historical Epic') --48
INSERT INTO Genres(GenreName) Values('Period Drama') --49
INSERT INTO Genres(GenreName) Values('Prison Drama') --50
INSERT INTO Genres(GenreName) Values('Teen Fantasy') --51
INSERT INTO Genres(GenreName) Values('Family') --52
INSERT INTO Genres(GenreName) Values('Holiday') --53

INSERT INTO Movies(MovieName, ReleaseYear, Length, Description, Rating, Image, Trailer, Director, Star, Writer, MultiDirectors, MultiStars, MultiWriters) Values('Iron Man', 2008, 126, 'After being held captive in an Afghan cave, billionaire engineer Tony Stark creates a unique weaponized suit of armor to fight evil.', 7.7, 'https://m.media-amazon.com/images/M/MV5BMTczNTI2ODUwOF5BMl5BanBnXkFtZTcwMTU0NTIzMw@@._V1_FMjpg_UX1000_.jpg', 'https://youtu.be/8ugaeA-nMTc?si=95Mfetqrv87fxv7Q', 'Jon Favreau', 'Robert Downey Jr., Gwyneth Paltrow, Terrence Howard', 'Mark Fergus, Hawk Ostby, Art Marcum', 0, 1, 1)
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
INSERT INTO Movies(MovieName, ReleaseYear, Length, Description, Rating, Image, Trailer, Director, Star, Writer, MultiDirectors, MultiStars, MultiWriters) Values('The Godfather', 1972, 175, 'The aging patriarch of an organized crime dynasty transfers control of his clandestine empire to his reluctant son.', 6.7, 'https://m.media-amazon.com/images/M/MV5BNGEwYjgwOGQtYjg5ZS00Njc1LTk2ZGEtM2QwZWQ2NjdhZTE5XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg', 'https://youtu.be/w3Wo6QiD3eU?si=uzCy5stqF3W9jVts', 'Francis Ford Coppola', 'Marlon Brando, Al Pacino, James Caan', 'Mario Puzo, Francis Ford Coppola', 0, 1, 1)
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
INSERT INTO Movies(MovieName, ReleaseYear, Length, Description, Rating, Image, Trailer, Director, Star, Writer, MultiDirectors, MultiStars, MultiWriters) Values('Pulp Fiction', 1994, 154, 'The lives of two mob hitmen, a boxer, a gangster and his wife, and a pair of diner bandits intertwine in four tales of violence and redemption.', 5, 'https://m.media-amazon.com/images/M/MV5BYTViYTE3ZGQtNDBlMC00ZTAyLTkyODMtZGRiZDg0MjA2YThkXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg', 'https://youtu.be/s7EdQ4FqbhY?si=oFUZl1vG1Kcad1h2', 'Quentin Tarantino', 'John Travolta, Uma Thurman, Samuel L. Jackson', 'Quentin Tarantino, Roger Avary', 0, 1, 1)
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
INSERT INTO Movies(MovieName, ReleaseYear, Length, Description, Rating, Image, Trailer, Director, Star, Writer, MultiDirectors, MultiStars, MultiWriters) Values('Spider-Man: Into the Spider-Verse', 2018, 117, 'Teen Miles Morales becomes the Spider-Man of his universe and must join with five spider-powered individuals from other dimensions to stop a threat for all realities.', 6.7, 'https://m.media-amazon.com/images/M/MV5BMjMwNDkxMTgzOF5BMl5BanBnXkFtZTgwNTkwNTQ3NjM@._V1_FMjpg_UX1000_.jpg', 'https://youtu.be/g4Hbz2jLxvQ?si=VXhc76ASqLjw19TD', 'Bob Persichetti, Peter Ramsey, Rodney Rothman', 'Shameik Moore, Jake Johnson, Hailee Steinfeld', 'Phil Lord, Rodney Rothman', 1, 1, 1)
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
INSERT INTO Movies(MovieName, ReleaseYear, Length, Description, Rating, Image, Trailer, Director, Star, Writer, MultiDirectors, MultiStars, MultiWriters) Values('The Batman', 2022, 176, 'When a sadistic serial killer begins murdering key political figures in Gotham, the Batman is forced to investigate the city''s hidden corruption and question his family''s involvement.', 4, 'https://m.media-amazon.com/images/M/MV5BMmU5NGJlMzAtMGNmOC00YjJjLTgyMzUtNjAyYmE4Njg5YWMyXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg', 'https://youtu.be/mqqft2x_Aa4?si=0c2fcENlkROEy09v', 'Jon Favreau', 'Robert Pattinson, Zoë Kravitz, Jeffrey Wright', 'Mark Fergus, Hawk Ostby, Art Marcum', 0, 1, 1)
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
INSERT INTO Movies(MovieName, ReleaseYear, Length, Description, Rating, Image, Trailer, Director, Star, Writer, MultiDirectors, MultiStars, MultiWriters) Values('Schindler''s List', 1993, 195, 'In German-occupied Poland during World War II, industrialist Oskar Schindler gradually becomes concerned for his Jewish workforce after witnessing their persecution by the Nazis.', 6, 'https://m.media-amazon.com/images/M/MV5BNjM1ZDQxYWUtMzQyZS00MTE1LWJmZGYtNGUyNTdlYjM3ZmVmXkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg', 'https://youtu.be/gG22XNhtnoY?si=QHkIZHY257CwQ8BE', 'Steven Spielberg', 'Liam Neeson, Ralph Fiennes, Ben Kingsley', 'Thomas Keneally, Steven Zaillian', 0, 1, 1)
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
INSERT INTO Movies(MovieName, ReleaseYear, Length, Description, Rating, Image, Trailer, Director, Star, Writer, MultiDirectors, MultiStars, MultiWriters) Values('Harry Potter and the Sorcerer''s Stone', 2001, 152, 'An orphaned boy enrolls in a school of wizardry, where he learns the truth about himself, his family and the terrible evil that haunts the magical world.', -1, 'https://m.media-amazon.com/images/M/MV5BNTU1MzgyMDMtMzBlZS00YzczLThmYWEtMjU3YmFlOWEyMjE1XkEyXkFqcGc@._V1_FMjpg_UX1000_.jpg', 'https://youtu.be/l91Km49W9qI?si=Sbi_cqLJ9ghp7kgy', 'Chris Columbus', 'Daniel Radcliffe, Rupert Grint, Emma Watson', 'J.K. Rowling, Steve Kloves', 0, 1, 1)
--Teen Fantasy
INSERT INTO GenresToMovies(MovieID, GenreID) Values(7,51)
--Adventure 
INSERT INTO GenresToMovies(MovieID, GenreID) Values(7,22)
--Family
INSERT INTO GenresToMovies(MovieID, GenreID) Values(7,52)
--Fantasy
INSERT INTO GenresToMovies(MovieID, GenreID) Values(7,17)
--Holiday
INSERT INTO GenresToMovies(MovieID, GenreID) Values(7,53)

INSERT INTO Reviews(UserID, MovieID, Rating, Description, ReviewDate) Values(1, 1, 9, 'A gripping experience from start to finish. The pacing was tight, and the emotional beats really landed. Highly recommended.', '2025-01-03')
INSERT INTO Reviews(UserID, MovieID, Rating, Description, ReviewDate) Values(2, 1, 6, 'It had its moments, but overall felt a bit uneven. Some strong scenes, though, and decent performances.', '2025-01-17')
INSERT INTO Reviews(UserID, MovieID, Rating, Description, ReviewDate) Values(3, 1, 8, 'Visually impressive and emotionally resonant. It strikes a nice balance between entertainment and depth.', '2025-01-28')

INSERT INTO Reviews(UserID, MovieID, Rating, Description, ReviewDate) Values(1, 2, 3, 'Unfortunately, it just didn’t work for me. The story was disjointed and I couldn’t connect with the characters.', '2025-02-05')
INSERT INTO Reviews(UserID, MovieID, Rating, Description, ReviewDate) Values(2, 2, 10,'An absolute masterpiece. Every element came together beautifully. Easily one of the best I''ve seen.', '2025-02-16')
INSERT INTO Reviews(UserID, MovieID, Rating, Description, ReviewDate) Values(3, 2, 7, 'Entertaining and well-crafted, though it played it a bit safe. Still, a solid watch.', '2025-02-27')

INSERT INTO Reviews(UserID, MovieID, Rating, Description, ReviewDate) Values(1, 3, 5, 'Right down the middle. It had potential, but didn’t quite capitalize on it. Some good ideas, but poor execution.', '2025-03-04')
INSERT INTO Reviews(UserID, MovieID, Rating, Description, ReviewDate) Values(2, 3, 2, 'Very disappointing. Weak script, flat characters, and a lack of focus. Hard to sit through.', '2025-03-15')
INSERT INTO Reviews(UserID, MovieID, Rating, Description, ReviewDate) Values(3, 3, 8, 'Strong storytelling with memorable moments. It may not be perfect, but it leaves a lasting impression.', '2025-03-29')

INSERT INTO Reviews(UserID, MovieID, Rating, Description, ReviewDate) Values(1, 4, 4, 'Started off with promise but quickly lost momentum. A few bright spots couldn’t save it.', '2025-04-02')
INSERT INTO Reviews(UserID, MovieID, Rating, Description, ReviewDate) Values(2, 4, 7, 'A well-made film with heart. Some flaws here and there, but still an enjoyable ride.', '2025-04-11')
INSERT INTO Reviews(UserID, MovieID, Rating, Description, ReviewDate) Values(3, 4, 9, 'Near-perfect in execution. A great example of how powerful cinema can be when everything clicks.', '2025-04-20')

INSERT INTO Reviews(UserID, MovieID, Rating, Description, ReviewDate) Values(1, 5, 1, 'A complete misfire. Nothing about this film worked. It felt like a waste of time.', '2025-04-26')
INSERT INTO Reviews(UserID, MovieID, Rating, Description, ReviewDate) Values(2, 5, 6, 'Not bad, not great. A few strong performances and decent direction, but lacking impact.', '2025-05-01')
INSERT INTO Reviews(UserID, MovieID, Rating, Description, ReviewDate) Values(3, 5, 5, 'Somewhat forgettable. There were moments of brilliance, but overall it didn’t leave much of an impression', '2025-05-06')

INSERT INTO Reviews(UserID, MovieID, Rating, Description, ReviewDate) Values(1, 6, 7, 'A solid entry in its genre. Fans will appreciate it, even if it doesn''t break new ground.', '2025-05-09')
INSERT INTO Reviews(UserID, MovieID, Rating, Description, ReviewDate) Values(2, 6, 3, 'It had some interesting visuals, but the plot and dialogue fell short. Hard to recommend.', '2025-05-15')
INSERT INTO Reviews(UserID, MovieID, Rating, Description, ReviewDate) Values(3, 6, 8, 'A satisfying watch. Well-paced and nicely acted, even if a bit predictable.', '2025-05-13')


Select*from Users
Select*from Genres
Select*from Genres ORDER BY GenreID ASC
Select*from Movies
Select*from GenresToMovies
Select*from Reviews
