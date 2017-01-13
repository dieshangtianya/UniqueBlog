USE UniqueBlogDB

IF EXISTS (SELECT * 
			FROM sysobjects
			WHERE name='sp_get_postlist_pagination' AND xtype='p')
DROP PROCEDURE sp_get_postlist_pagination
GO

CREATE PROCEDURE sp_get_postlist_pagination
(
@PageIndex int,
@PageSize int
)
AS
BEGIN 
	SET NOCOUNT ON
	DECLARE @sql nvarchar(2000)
	DECLARE @fields nvarchar(500)
	DECLARE @where nvarchar(500)

	SET @fields='BlogPostId,BlogId,PostTitle,PostContent,PostPlainContent,CreatedDate,LastUpdatedDate,Tags '
	SET @where= 'WHERE 1=1'
	IF(@BlogId==null)
	SET @sql='SELECT TOP '+str(@pageSize)+' '+@fields+' FROM t_blog_post WHERE BlogPostId NOT IN (SELECT TOP '+STR(@pageIndex*@pageSize)+' BlogPostId FROM t_blog_post ORDER BY BlogPostID DESC) ORDER BY BlogPostID DESC'

	EXEC (@sql)

	SET NOCOUNT OFF
END

