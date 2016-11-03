--===========================================
--创建数据库脚本
--===========================================

IF EXISTS (SELECT * 
	FROM master..sysdatabases 
	WHERE name='UniqueBlogDB')
DROP DATABASE UniqueBlogDB
GO

CREATE DATABASE UniqueBlogDB
GO

