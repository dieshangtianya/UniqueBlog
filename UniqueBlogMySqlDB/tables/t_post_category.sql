/************************************************
**script used to create the table 't_post_category'
************************************************/

USE UniqueBlogDB;

DROP TABLE IF EXISTS t_post_category;


CREATE TABLE t_post_category
(
	Id INT NOT NULL AUTO_INCREMENT,
	PostId INT NOT NULL,
	CategoryId INT NOT NULL,

	CONSTRAINT pk_Post_Category_ID PRIMARY KEY(Id),
	CONSTRAINT fk_Blog_Post_ID FOREIGN KEY (PostId)REFERENCES t_blog_post(BlogPostId),
	CONSTRAINT fk_Category_ID FOREIGN KEY(CategoryId) REFERENCES t_category(CategoryId)
)DEFAULT CHARSET=utf8;