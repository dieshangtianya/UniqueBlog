use UniqueBlogDB;

IF EXISTS (SELECT * 
			FROM sysobjects
			WHERE name='sp_add_blogpost_relation' AND xtype='p')
DROP PROCEDURE sp_add_blogpost_relation
GO

CREATE PROCEDURE sp_add_blogpost_relation
(
	@postCategoryTable udt_post_category_relationship readonly
)
AS
BEGIN
	INSERT INTO t_post_category(PostId,CategoryId) 
	SELECT * FROM @postCategoryTable
END
