USE UniqueBlogDB;

DROP PROCEDURE IF EXISTS sp_update_blogpost;

CREATE PROCEDURE sp_update_blogpost
(
IN PostId INT,
IN PostTitle VARCHAR(100),
IN PostContent TEXT,
IN PostPlainContent TEXT,
IN Tags VARCHAR(100),
IN Draft TINYINT(1)
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
					 Draft=Draft,Tags=Tags, LastUpdatedDate=LastUpdatedDate WHERE BlogPostId=PostId;

		/*update blog post category list*/
	COMMIT;
	
END;