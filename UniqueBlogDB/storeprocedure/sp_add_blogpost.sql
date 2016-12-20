use UniqueBlogDB;

IF EXISTS (SELECT * 
			FROM sysobjects
			WHERE name='sp_addblogpost' AND xtype='p')
DROP PROCEDURE sp_addblogpost
GO

CREATE PROCEDURE sp_addblogpost
(
@BlogId int,
@PostTitle nvarchar(100),
@PostContent ntext,
@CreatedDate datetime,
@Tags nvarchar(100)
)
AS
BEGIN
	INSERT INTO t_blog_post(BlogId,PostTitle,PostContent,CreatedDate,Tags) Values (@BlogId,@PostTitle,@PostContent,@CreatedDate,@Tags)
END
