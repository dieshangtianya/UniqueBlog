use UniqueBlogDB;

DROP PROCEDURE IF EXISTS sp_add_comment;

CREATE PROCEDURE sp_add_comment
(
BlogId INT,
PostId INT,
UserId INT,
UserName VARCHAR(20),
CommentContent TEXT,
CreatedDate DATETIME
)
BEGIN
	INSERT INTO t_comment(BlogId,PostId,UserId,UserName,CommentContent,CreatedDate) Values (BlogId,PostId,UserId,UserName,CommentContent,CreatedDate);
	SELECT LAST_INSERT_ID();
END