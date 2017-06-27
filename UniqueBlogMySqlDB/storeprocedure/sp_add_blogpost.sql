use UniqueBlogDB;

DROP PROCEDURE IF EXISTS sp_add_blogpost;

CREATE PROCEDURE sp_add_blogpost
(
BlogId int,
PostTitle VARCHAR(100),
PostContent TEXT,
PostPlainContent TEXT,
CreatedDate DATETIME,
LastUpdatedDate DATETIME,
Tags VARCHAR(100)
)
BEGIN
	INSERT INTO t_blog_post(BlogId,PostTitle,PostContent,PostPlainContent,CreatedDate,LastUpdatedDate,Tags) 
	Values (BlogId,PostTitle,PostContent,PostPlainContent,CreatedDate,LastUpdatedDate,Tags);
	
	SELECT LAST_INSERT_ID();
END