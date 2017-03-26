/*****************************************
***script used to create the table 't_blog'
*****************************************/

USE UniqueBlogDB;

DROP TABLE IF EXISTS t_blog;

CREATE TABLE t_blog
(
	BlogId INT NOT NULL AUTO_INCREMENT,
	BlogTitle NVARCHAR(50) NOT NULL, 
	`Description` NVARCHAR(200) NULL,
	UserId INT NOT NULL,
	CreationDate DATETIME NOT NULL,
	CONSTRAINT pk_BlogId PRIMARY KEY(BlogId),
	CONSTRAINT fk_UserId FOREIGN KEY(UserId) REFERENCES t_user(UserId)
)DEFAULT CHARSET=utf8;