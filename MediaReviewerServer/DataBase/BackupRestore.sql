

Use master
Go

-- Create a login for the admin user
CREATE LOGIN [MediaReviewerAdminLogin] WITH PASSWORD = 'pass031206';
Go

--so user can restore the DB!
ALTER SERVER ROLE sysadmin ADD MEMBER [MediaReviewerAdminLogin];
Go



Create Database MediaReviewerDB
Go

--use MediaReviewerDB
--Go

---- Create a user in the TasksManagementDB database for the login
--CREATE USER [MediaReviewerUser] FOR LOGIN [MediaReviewerAdminLogin];
--Go
---- Add the user to the db_owner role to grant admin privileges
--ALTER ROLE db_owner ADD MEMBER [MediaReviewerAdminUser];
--Go


--                USE master;
--                ALTER DATABASE MediaReviewerDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
--                RESTORE DATABASE MediaReviewerDB FROM DISK = 'C:\Users\User\Source\Repos\Lettuce4ever\MediaReviewerServer\MediaReviewerServer\DataBase\backup.bak' WITH REPLACE;
--                ALTER DATABASE MediaReviewerDB SET MULTI_USER;


                