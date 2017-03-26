/******************************************
**script of creating database
**for mysql database there is an characterset setting
**there are four levels setting for character, the are:
**(1)server (2)database (3)table (4)connection
**we can change this in 'database','table','column', and others like server,connection
**however, we should set it utf-8 to support chinese sometimes
**so we should set it to utf-8 while setting up the mysql database
*******************************************/

DROP DATABASE IF EXISTS UniqueBlogDB;

/*all the tables in the database are utf-8*/
CREATE DATABASE UniqueBlogDB DEFAULT CHARACTER SET utf8 COLLATE utf8_general_ci;