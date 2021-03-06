﻿use UniqueBlogDB;

IF EXISTS (SELECT * 
			FROM sysobjects
			WHERE name='sp_add_comment' AND xtype='p')
DROP PROCEDURE sp_add_comment
GO

CREATE PROCEDURE sp_add_comment
(
@BlogId int,
@PostId int,
@UserId int,
@UserName nvarchar(20),
@CommentContent ntext,
@CreatedDate datetime
)
AS
BEGIN
	INSERT INTO t_comment(BlogId,PostId,UserId,UserName,CommentContent,CreatedDate) Values (@BlogId,@PostId,@UserId,@UserName,@CommentContent,@CreatedDate);
	SELECT Scope_Identity();
END
