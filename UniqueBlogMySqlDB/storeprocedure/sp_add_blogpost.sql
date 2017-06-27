use UniqueBlogDB;

DROP PROCEDURE IF EXISTS sp_add_blogpost;

CREATE PROCEDURE sp_add_blogpost
(
BlogId INT,
PostTitle VARCHAR(100),
PostContent TEXT,
PostPlainContent TEXT,
CreatedDate DATETIME,
LastUpdatedDate DATETIME,
Draft TINYINT(1),
Tags VARCHAR(100)
)
BEGIN
	INSERT INTO t_blog_post(BlogId,PostTitle,PostContent,PostPlainContent,CreatedDate,LastUpdatedDate,Draft,Tags) 
	Values (BlogId,PostTitle,PostContent,PostPlainContent,CreatedDate,LastUpdatedDate,Draft,Tags);
	
	SELECT LAST_INSERT_ID();
END