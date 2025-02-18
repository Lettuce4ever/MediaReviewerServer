
Use master
Go

-- Create a login for the admin user
CREATE LOGIN [MediaReviewerAdminLogin] WITH PASSWORD = 'gal031206';
Go

--so user can restore the DB!
ALTER SERVER ROLE sysadmin ADD MEMBER [MediaReviewerAdminLogin];
Go
