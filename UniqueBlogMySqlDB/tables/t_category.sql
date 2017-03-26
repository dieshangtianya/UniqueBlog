/*****************************************
***script used to create the table 't_category'
*****************************************/

USE UniqueBlogDB;

drop table if exists t_category;

CREATE TABLE t_category
(
	CategoryId INT NOT NULL AUTO_INCREMENT,
	BlogId INT NOT NULL,
	CategoryName NVARCHAR(50) NOT NULL,
	`Description` NVARCHAR(200) NULL,
	CreatedDate TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
	CONSTRAINT pk_Category PRIMARY KEY(CategoryId),
	CONSTRAINT fk_BlogId FOREIGN KEY(BlogId) REFERENCES t_blog(BlogId)
)DEFAULT CHARSET=utf8;