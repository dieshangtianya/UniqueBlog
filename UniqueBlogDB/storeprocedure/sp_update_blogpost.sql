use UniqueBlogDB;

IF EXISTS (SELECT * 
			FROM sysobjects
			WHERE name='sp_update_blogpost' AND xtype='p')
DROP PROCEDURE sp_update_blogpost
GO

CREATE PROCEDURE sp_update_blogpost
(
@PostId int,
@PostTitle nvarchar(100),
@PostContent ntext,
@PostPlainContent ntext,
@Tags nvarchar(100),
@LastUpdatedDate datetime,
@Draft tinyint(1),
@PostCategoryTable udt_post_category_relationship READONLY
)
AS
BEGIN TRY
	BEGIN TRAN;
		DECLARE @updatedCategoryCount int =0;
		
		/**Update the blog post table**/
		UPDATE t_blog_post SET PostTitle=@PostTitle, PostContent=@PostContent, PostPlainContent=@PostPlainContent, Draft=@Draft, Tags=@Tags, LastUpdatedDate=@LastUpdatedDate WHERE BlogPostId=@PostId
		
		/**Clear the blog post category list**/
		SELECT @updatedCategoryCount =COUNT(*) FROM @PostCategoryTable
		IF @updatedCategoryCount>0
			DELETE FROM t_post_category WHERE PostId=@PostId;
			INSERT INTO t_post_category(PostId,CategoryId) SELECT * FROM @postCategoryTable
	COMMIT TRAN;
END TRY
BEGIN CATCH
	IF @@TRANCOUNT>0
		ROLLBACK TRAN
END CATCH
