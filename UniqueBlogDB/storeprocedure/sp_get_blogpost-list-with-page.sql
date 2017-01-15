USE UniqueBlogDB

IF EXISTS (SELECT * 
			FROM sysobjects
			WHERE name='sp_get_postlist_pagination' AND xtype='p')
DROP PROCEDURE sp_get_postlist_pagination
GO

CREATE PROCEDURE sp_get_postlist_pagination
(
@CategoryId int,
@DateStart Datetime,
@DateEnd Datetime,
@PageIndex int,
@PageSize int,
@TotalRecordAmount int OUTPUT
)
AS
BEGIN
	DECLARE @TableName varchar(50)
	DECLARE @Fields varchar(1000)
	DECLARE @SqlWhere varchar(1000)
	DECLARE @OrderByFields varchar(50)

	SET NOCOUNT ON
		IF(@CategoryId IS NOT NULL)
			SET @SqlWhere +=' CategoryID= ' + @CategoryId + ' '
		IF(@DateStart IS NOT NULL AND @DateEnd IS NOT NULL)
			SET @SqlWhere += ' CreatedDate > '+@DateStart + ' AND CreatedDate < ' + @DateEnd + ' '

		SET @Fields=' BlogPostID, BlogId, PostTitle, PostContent, PostPlainContent, CreatedDate, LastUpdatedDate, Tags'
		SET @OrderByFields=' ORDER BY BlogId DESC '
		SET @TableName='t_blog_post'

		EXEC sp_get_items_super_pagination @TableName,@Fields,@SqlWhere,'', @OrderByFields,@PageIndex,@PageSize,@TotalRecordAmount OUTPUT
	SET NOCOUNT OFF
END