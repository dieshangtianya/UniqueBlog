USE UniqueBlogDB;

DROP PROCEDURE IF EXISTS sp_update_blogpost;

CREATE PROCEDURE sp_update_blogpost
(
IN PostId int,
IN PostTitle NVARCHAR(100),
IN PostContent TEXT,
IN PostPlainContent TEXT,
IN Tags NVARCHAR(100),
IN LastUpdatedDate DATETIME
)
BEGIN
	DECLARE updatedCategoryCount int DEFAULT 0;
	
	DECLARE EXIT HANDLER FOR SQLEXCEPTION
	BEGIN
		ROLLBACK;
	END;

	START TRANSACTION;
		/*update the blog post*/
		UPDATE t_blog_post SET PostTitle=PostTitle, PostContent=PostContent, PostPlainContent=PostPlainContent, 
					 Tags=Tags, LastUpdatedDate=LastUpdatedDate WHERE BlogPostId=PostId;

		/*update blog post category list*/
	COMMIT;
	
END;