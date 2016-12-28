/*===================================
Tips:
There are three ways to avoid the messy code in SQL SERVER database.

(1) Create database with the right collation.
(2) Specify the collation on the database.
(3) If we use the SQL SERVER database which uses the english platform, if we insert chinese, it will show with mojibake. 
	so here we can insert values prefix with 'N'. First of all we should change the db table column type to nvarchar which column potentially contains chinese.

=====================================*/

use UniqueBlogDB;

--t_user
INSERT INTO t_user (UserName,Email,[Password],NickName) VALUES ('frwang','woheaven@sina.com','3748511','蝶殇天涯');
GO
--t_blog
INSERT INTO t_blog (BlogTitle,[Description],UserId,CreationDate) VALUES ('不停地走着','沿着曲折的路坚持走下去，不停歇，不放弃',1,'2016/09/10');
GO
--t_category
DECLARE @blogId INT;
SELECT @blogId=(SELECT TOP 1 BlogId FROM t_blog);
INSERT INTO t_category (BlogId,CategoryName,[Description],CreatedDate) VALUES (@blogId,'C#基础',NULL,GETDATE());
INSERT INTO t_category (BlogId,CategoryName,[Description],CreatedDate) VALUES (@blogId,'Winformm编程',NULL,GETDATE());
INSERT INTO t_category (BlogId,CategoryName,[Description],CreatedDate) VALUES (@blogId,'ASP.NET',NULL,GETDATE());
INSERT INTO t_category (BlogId,CategoryName,[Description],CreatedDate) VALUES (@blogId,'ASP.NET MVC',NULL,GETDATE());
INSERT INTO t_category (BlogId,CategoryName,[Description],CreatedDate) VALUES (@blogId,'WPF',NULL,GETDATE());
INSERT INTO t_category (BlogId,CategoryName,[Description],CreatedDate) VALUES (@blogId,'Entity Framework',NULL,GETDATE());
INSERT INTO t_category (BlogId,CategoryName,[Description],CreatedDate) VALUES (@blogId,'.NET设计模式',NULL,GETDATE());
INSERT INTO t_category (BlogId,CategoryName,[Description],CreatedDate) VALUES (@blogId,'AngularJS',NULL,GETDATE());
INSERT INTO t_category (BlogId,CategoryName,[Description],CreatedDate) VALUES (@blogId,'WEB前端',NULL,GETDATE());
GO

--t_blog_post
--DECLARE @blogId INT;
--SELECT @blogId=(SELECT TOP 1 BlogId FROM t_blog);

--INSERT INTO t_blog_post(BlogId,PostTitle) VALUES (@blogId,'Asp.net MVC 分部视图');
--INSERT INTO t_blog_post(BlogId,PostTitle) VALUES (@blogId,'Asp.net MVC 路由机制');
--INSERT INTO t_blog_post(BlogId,PostTitle) VALUES (@blogId,'Asp.net MVC 模型处理');
--INSERT INTO t_blog_post(BlogId,PostTitle) VALUES (@blogId,'Asp.net MVC 客户端验证');
--INSERT INTO t_blog_post(BlogId,PostTitle) VALUES (@blogId,'Asp.net MVC 服务器验证');
--INSERT INTO t_blog_post(BlogId,PostTitle) VALUES (@blogId,'Asp.net MVC 部署');

--GO

----t_post_category
--DECLARE @categoryId INT,
--		@index INT;

--SELECT @categoryId=(SELECT TOP 1 CategoryId FROM t_category);
--SELECT @index=0;

--WHILE(@index<5)
--BEGIN
--INSERT INTO t_post_category (PostId,CategoryId) values(@index+1,@categoryId);
--SELECT @index=@index+1;
--END

--GO



