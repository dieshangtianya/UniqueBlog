use UniqueBlogDB;

DROP function IF EXISTS func_split_str;

CREATE function func_split_str
(
str VARCHAR(10000),
delim VARCHAR(10),
pos INT
)

RETURNS varchar(10000)

RETURN REPLACE(
       SUBSTRING(SUBSTRING_INDEX(str, delim, pos),LENGTH(SUBSTRING_INDEX(str, delim, pos -1)) + 1),
       delim, 
       '');