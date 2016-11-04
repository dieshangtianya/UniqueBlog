--===========================================
--创建数据库脚本
--===========================================

IF EXISTS (SELECT * 
	FROM master..sysdatabases 
	WHERE name='UniqueBlogDB')

BEGIN
ALTER DATABASE UniqueBlogDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
DROP DATABASE UniqueBlogDB
END

GO

CREATE DATABASE UniqueBlogDB
COLLATE Chinese_PRC_CI_AS
GO

