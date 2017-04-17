drop PROCEDURE if EXISTS sp_get_items_super_pagination;
create procedure sp_get_items_super_pagination(
IN TableName VARCHAR(5000),
IN `Fields` VARCHAR(5000),
IN SqlWhere VARCHAR(5000),  
IN GroupFields VARCHAR(5000),
IN OrderByFields VARCHAR(5000),
IN PageIndex INT,
IN PageSize INT,
OUT TotalRecordAmount INT
)
BEGIN

/**declare two parameter to store the total page amount and sql which will be executed later**/
DECLARE TotalPageAmount INT DEFAULT 0;

DECLARE StartRecord INT DEFAULT 0;
DECLARE EndRecord INT DEFAULT 0;

SET @SqlCommand='';

IF SqlWhere IS NOT NULL AND LENGTH(SqlWhere)>0 AND LOCATE('WHERE',UPPER(LTRIM(SqlWhere)))!=1 THEN
	SET SqlWhere = CONCAT(' WHERE ',SqlWhere);
END IF; 

IF GroupFields IS NOT NULL AND LENGTH(GroupFields)>0 AND LOCATE('GROUP BY',UPPER(LTRIM(GroupFields))) != 1 THEN
		SET GroupFields = CONCAT(' GROUP BY ', GroupFields);
END IF;

IF OrderByFields IS NOT NULL AND LENGTH(OrderByFields)>0 AND LOCATE('ORDER BY',UPPER(LTRIM(OrderByFields)))!=1 THEN
		SET OrderByFields=CONCAT(' ORDER BY ' , OrderByFields);
END IF;

IF SqlWhere IS NULL OR SqlWhere='' THEN
	IF GroupFields IS NULL OR GroupFields='' THEN
		/*if SqlWhere and GroupFields are both null or empty*/
		SET @SqlCommand=CONCAT('SELECT @TotalRecords:= COUNT(*) FROM ' , TableName);
	ELSE
		/*if SqlWhere is null but GroupFields is not null*/
		SET @SqlCommand=CONCAT('SELECT @TotalRecords:= COUNT(*) FROM (SELECT ', `Fields`,' FROM ' , TableName , ' ' , GroupFields, ') AS tempTb');
	END IF;
ELSE
	IF GroupFields IS NULL OR GroupFields='' THEN
		/*if SqlWhere is not null or empty, but the GroupFields is null or empty*/
		SET @SqlCommand=CONCAT('SELECT @TotalRecords:= COUNT(*) FROM ' , TableName , ' ' , SqlWhere);
	ELSE
		/*if SqlWhere and GroupFields both are not null or empty*/
		SET @SqlCommand=CONCAT('SELECT @TotalRecords:= COUNT(*) FROM (SELECT ', `Fields` , ' FROM ' , TableName , ' ' , SqlWhere , ' ',GroupFields , ') AS tempTb');
	END IF;
END IF;

PREPARE stmt FROM @SqlCommand;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

SET TotalRecordAmount:=@TotalRecords;
SELECT TotalRecordAmount;

/*get the total page*/
SET TotalPageAmount=CEILING((TotalRecordAmount+0.0)/PageSize);
SELECT TotalPageAmount;


END