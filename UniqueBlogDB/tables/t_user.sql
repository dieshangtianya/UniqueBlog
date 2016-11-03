--========================================
--创建数据表t_user脚本
--========================================

USE UniqueBlogDB;

IF EXISTS (SELECT *
	FROM sysobjects 
	WHERE name='t_user' AND type='U')
DROP TABLE t_user;
GO


CREATE TABLE t_user
(
	UserId INT NOT NULL IDENTITY(1,1) CONSTRAINT pk_UserId PRIMARY KEY, 
    UserName VARCHAR(50) NOT NULL, 
    Email VARCHAR(100) NULL, 
    [Password] VARCHAR(20) NULL, 
    NickName VARCHAR(50) NOT NULL
)