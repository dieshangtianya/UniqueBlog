--========================================
--创建博客文章t_blog_post表脚本
--========================================

use UniqueBlogDB;
GO

IF EXISTS ( SELECT * 
			FROM sysobjects
			WHERE name='t_blog_post' AND type='u')
DROP TABLE t_blog_post;
GO

CREATE TABLE t_blog_post
(
	BlogPostId INT NOT NULL IDENTITY(1,1) CONSTRAINT pk_BlogPostId PRIMARY KEY,
	BlogId INT NOT NULL FOREIGN KEY REFERENCES t_blog(BlogId),
	PostTitle NVARCHAR(100) NOT NULL,
	PostContent NTEXT NOT NULL DEFAULT(''),
	PostPlainContent NTEXT NOT NULL DEFAULT(''),
	CreatedDate DATETIME NOT NULL DEFAULT(GETDATE()),
	LastUpdatedDate DATETIME NULL DEFAULT(GETDATE()),
	Tags NVARCHAR(100) NULL,
	VisitAmount INT DEFAULT(0) NOT NULL
)
GO