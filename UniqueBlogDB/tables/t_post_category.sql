--===============================================
--创建博客文章分类t_post_category表脚本
--===============================================

USE UniqueBlogDB;
GO

IF EXISTS ( SELECT * 
	FROM sysobjects
	WHERE name='t_post_category' AND type='U')
DROP TABLE t_category;
GO

CREATE TABLE t_post_category
(
	Id INT IDENTITY(1,1) CONSTRAINT pk_Post_Category_ID PRIMARY KEY,
	PostId INT NOT NULL FOREIGN KEY REFERENCES t_blog_post(BlogPostId),
	CategoryId INT NOT NULL FOREIGN KEY REFERENCES t_category(CategoryId),
);
GO