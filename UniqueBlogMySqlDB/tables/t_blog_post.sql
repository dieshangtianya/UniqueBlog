/*****************************************
**script used to create the table 't_blog_post'
*****************************************/

use UniqueBlogDB;

DROP TABLE IF EXISTS t_blog_post;

CREATE TABLE t_blog_post
(
	BlogPostId INT NOT NULL AUTO_INCREMENT,
	BlogId INT NOT NULL,
	PostTitle NVARCHAR(100) NOT NULL,
	PostContent TEXT NOT NULL,
	PostPlainContent TEXT NOT NULL,
	CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
	LastUpdatedDate DATETIME NULL DEFAULT CURRENT_TIMESTAMP,
	Draft TINYINT(1) NOT NULL DEFAULT 0,
	Tags NVARCHAR(100) NULL,
	CONSTRAINT pk_BlogPostId PRIMARY KEY(BlogPostId),
	CONSTRAINT fk_Blog_Post_BlogId FOREIGN KEY(BlogId) REFERENCES t_blog(BlogId)
)DEFAULT CHARSET=utf8;