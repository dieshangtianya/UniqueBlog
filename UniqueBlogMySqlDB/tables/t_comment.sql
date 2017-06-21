/*************************************
** script used to create the table 't_comment'
*************************************/

USE UniqueBlogDB;

DROP TABLE IF EXISTS t_comment;

CREATE TABLE t_comment
(
	CommentId INT NOT NULL AUTO_INCREMENT,
	BlogId INT NOT NULL,
	PostId INT NOT NULL ,
	UserName NVARCHAR(20) NOT NULL,
	UserId INT NULL,
	CommentContent TEXT NULL,
	CreatedDate DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

	CONSTRAINT pk_CommentId PRIMARY KEY(CommentId),
	CONSTRAINT fk_BlogId FOREIGN KEY(BlogId) REFERENCES t_blog(BlogId),
	CONSTRAINT FK_Comment_Blog_PostId FOREIGN KEY(PostId) REFERENCES t_blog_post(BlogPostId)
)