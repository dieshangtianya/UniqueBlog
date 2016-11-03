use UniqueBlogDB;

--t_user
INSERT INTO t_user (UserName,Email,[Password],NickName) values('frwang','woheaven@sina.com','3748511','蝶殇天涯');
GO
--t_blog
INSERT INTO t_blog (BlogTitle,[Description],UserId,CreationDate) values('不停地走着','沿着曲折的路坚持走下去，不停歇，不放弃',1,'2016/09/10');
GO
--t_category
declare @blogId int;
select @blogId=(select top 1 BlogId from t_blog);
INSERT INTO t_category (BlogId,CategoryName,[Description],CreatedDate) values(@blogId,'C#基础',NULL,GETDATE());
INSERT INTO t_category (BlogId,CategoryName,[Description],CreatedDate) values(@blogId,'Winformm编程',NULL,GETDATE());
INSERT INTO t_category (BlogId,CategoryName,[Description],CreatedDate) values(@blogId,'ASP.NET',NULL,GETDATE());
INSERT INTO t_category (BlogId,CategoryName,[Description],CreatedDate) values(@blogId,'ASP.NET MVC',NULL,GETDATE());
INSERT INTO t_category (BlogId,CategoryName,[Description],CreatedDate) values(@blogId,'WPF',NULL,GETDATE());
INSERT INTO t_category (BlogId,CategoryName,[Description],CreatedDate) values(@blogId,'Entity Framework',NULL,GETDATE());
INSERT INTO t_category (BlogId,CategoryName,[Description],CreatedDate) values(@blogId,'.NET设计模式',NULL,GETDATE());
INSERT INTO t_category (BlogId,CategoryName,[Description],CreatedDate) values(@blogId,'AngularJS',NULL,GETDATE());
INSERT INTO t_category (BlogId,CategoryName,[Description],CreatedDate) values(@blogId,'WEB前端',NULL,GETDATE());
GO