use UniqueBlogDB;

IF EXISTS (SELECT * 
			FROM sysobjects
			WHERE name='sp_add_blogpost' AND xtype='p')
DROP PROCEDURE sp_add_blogpost
GO

CREATE PROCEDURE sp_add_blogpost
(
@BlogId int,
@PostTitle nvarchar(100),
@PostContent ntext,
@PostPlainContent ntext,
@CreatedDate datetime,
@Tags nvarchar(100)
)
AS
BEGIN
	INSERT INTO t_blog_post(BlogId,PostTitle,PostContent,PostPlainContent,CreatedDate,Tags) Values (@BlogId,@PostTitle,@PostContent,@PostPlainContent,@CreatedDate,@Tags);
	SELECT Scope_Identity();
END
