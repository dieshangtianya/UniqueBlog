/******************************************
**script used to create the table 't_user'
******************************************/

USE UniqueBlogDB;

DROP TABLE IF EXISTS t_user;

CREATE TABLE t_user
(
	UserId INT NOT NULL AUTO_INCREMENT, 
    UserName VARCHAR(50) NOT NULL, 
    Email VARCHAR(100) NULL, 
    `Password` VARCHAR(20) NULL, 
    NickName VARCHAR(50) NOT NULL,
	CONSTRAINT pk_UserId PRIMARY KEY(UserId)
)DEFAULT CHARSET=utf8;