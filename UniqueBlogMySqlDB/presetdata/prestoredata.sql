/*===================================
Tips:
There are three ways to avoid the messy code in SQL SERVER database.

(1) Create database with the right collation.
(2) Specify the collation on the database.
(3) If we use the SQL SERVER database which uses the english platform, if we insert chinese, it will show with mojibake. 
	so here we can insert values prefix with 'N'. First of all we should change the db table column type to nvarchar which column potentially contains chinese.

=====================================*/

use UniqueBlogDB;

/**t_user**/
INSERT INTO t_user (UserName,Email,`Password`,NickName) VALUES ('frwang','woheaven@sina.com','3748511','蝶殇天涯');

/***t_blog**/
INSERT INTO t_blog (BlogTitle,`Description`,UserId,CreationDate) VALUES ('不停地走着','沿着曲折的路坚持走下去，不停歇，不放弃',1,'2016/09/10');
