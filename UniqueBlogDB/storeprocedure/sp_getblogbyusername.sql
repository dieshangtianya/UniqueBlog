use UniqueBlogDB;

IF EXISTS (SELECT * 
			FROM sysobjects
			WHERE name='sp_getblogbyusername' AND xtype='p')
DROP PROCEDURE sp_getblogbyusername
GO

CREATE PROCEDURE sp_getblogbyusername
@UserName varchar(100)
AS
BEGIN
SELECT * FROM t_blog WHERE UserId=(SELECT UserId FROM t_user WHERE UserName=@UserName)
END