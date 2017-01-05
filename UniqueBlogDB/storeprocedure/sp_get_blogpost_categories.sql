use UniqueBlogDB;

IF EXISTS (SELECT * 
			FROM sysobjects
			WHERE name='sp_get_blogpost_categories' AND xtype='p')
DROP PROCEDURE sp_get_blogpost_categories
GO

CREATE PROCEDURE sp_get_blogpost_categories
(
@postId INT
)
AS
BEGIN
	SELECT CategoryId,BlogId,CategoryName,[Description],CreatedDate FROM t_Category A
		WHERE EXISTS (SELECT CategoryId FROM t_post_category B WHERE B.PostId=@postId and B.CategoryId=A.CategoryId)
END
