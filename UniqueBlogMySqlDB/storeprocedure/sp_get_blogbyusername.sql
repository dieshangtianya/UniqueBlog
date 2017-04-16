use UniqueBlogDB;

DROP PROCEDURE IF EXISTS sp_getblogbyusername;

CREATE PROCEDURE sp_getblogbyusername
(
UserName varchar(100)
)
BEGIN
SELECT * FROM t_blog WHERE UserId=(SELECT UserId FROM t_user WHERE UserName=UserName);
END