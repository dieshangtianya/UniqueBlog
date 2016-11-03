--========================================
--创建数据表t_blog脚本
--========================================

USE UniqueBlogDB;

IF EXISTS (SELECT *
	FROM sysobjects 
	WHERE name='t_blog' AND type='U')
DROP TABLE t_user;
GO


CREATE TABLE t_blog
(
	BlogId INT NOT NULL IDENTITY(1,1) CONSTRAINT pk_blogId PRIMARY KEY, 
    BlogTitle VARCHAR(50) NOT NULL, 
    [Description] VARCHAR(200) NULL,
	UserId INT NOT NULL,
    CreationDate datetime NOT NULL,
	--添加外键约束
	FOREIGN KEY(UserId) REFERENCES t_user(UserId)
)