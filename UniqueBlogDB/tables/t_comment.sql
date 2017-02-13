--========================================
--创建博客t_comment表脚本
--========================================

USE UniqueBlogDB;

IF EXISTS (SELECT * 
	FROM sysobjects 
	WHERE name='t_comment' AND type='U')
DROP TABLE t_comment;
GO

CREATE TABLE t_comment
(
	CommentId INT NOT NULL IDENTITY(1,1) CONSTRAINT pk_commentId PRIMARY KEY,
	BlogId INT NOT NULL FOREIGN KEY REFERENCES t_blog(BlogId),
	PostId INT NOT NULL FOREIGN KEY REFERENCES t_blog_post(BlogPostId),
	UserName NVARCHAR(20) NOT NULL,
	UserId INT NULL,
	CommentContent NTEXT NULL DEFAULT(''),
	CreatedDate DATETIME NOT NULL DEFAULT(GETDATE())
)