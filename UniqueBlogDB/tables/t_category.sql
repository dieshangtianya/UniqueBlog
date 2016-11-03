--===============================================
--创建博客分类表
--===============================================

USE UniqueBlogDB;
GO

IF EXISTS ( SELECT * 
	FROM sysobjects
	WHERE name='t_category' AND type='U')
DROP TABLE t_category;
GO

CREATE TABLE t_category
(
	CategoryId INT IDENTITY(1,1) CONSTRAINT pk_Category PRIMARY KEY,
	BlogId INT NOT NULL,
	CategoryName VARCHAR(50) NOT NULL,
	[Description] VARCHAR(200) NULL,
	CreatedDate Datetime NOT NULL DEFAULT GETDATE()
);
GO