﻿drop PROCEDURE if EXISTS sp_get_items_super_pagination;

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
		SET @SqlCommand=CONCAT('SET @TotalRecords= (SELECT COUNT(*) FROM ' , TableName,')');
	ELSE
		/*if SqlWhere is null but GroupFields is not null*/
		SET @SqlCommand=CONCAT('SET @TotalRecords= (SELECT COUNT(*) FROM (SELECT ', `Fields`,' FROM ' , TableName , ' ' , GroupFields, ') AS tempTb)');
	END IF;
ELSE
	IF GroupFields IS NULL OR GroupFields='' THEN
		/*if SqlWhere is not null or empty, but the GroupFields is null or empty*/
		SET @SqlCommand=CONCAT('SET @TotalRecords= (SELECT COUNT(*) FROM ' , TableName , ' ' , SqlWhere,')');
	ELSE
		/*if SqlWhere and GroupFields both are not null or empty*/
		SET @SqlCommand=CONCAT('SET @TotalRecords= (SELECT COUNT(*) FROM (SELECT ', `Fields` , ' FROM ' , TableName , ' ' , SqlWhere , ' ',GroupFields , ') AS tempTb)');
	END IF;
END IF;

PREPARE stmt FROM @SqlCommand;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

SET TotalRecordAmount=@TotalRecords;

/*get the total page*/
SET TotalPageAmount=CEILING((TotalRecordAmount+0.0)/PageSize);

SET @row_number=0;

IF SqlWhere IS NULL OR SqlWhere='' THEN
	IF GroupFields IS NULL OR GroupFields='' THEN
		SET @SqlCommand= CONCAT('SELECT * FROM (SELECT @row_number:=@row_number+1 AS rowId,',`Fields`,' FROM ',TableName);
	ELSE
		SET @SqlCommand=CONCAT('SELECT * FROM (SELECT @row_number:=@row_number+1 AS rowId,',`Fields`,' FROM ',TableName,' ',GroupFields);
	END IF;
ELSE
	IF GroupFields IS NULL OR GroupFields='' THEN
		SET @SqlCommand=CONCAT('SELECT * FROM (SELECT @row_number:=@row_number+1 AS rowId,',`Fields`,' FROM ',TableName,' ',SqlWhere);
	ELSE
		SET @SqlCommand=CONCAT('SELECT * FROM (SELECT @row_number:=@row_number+1 AS rowId,',`Fields`,' FROM ',TableName,' ',SqlWhere , ' ',GroupFields);
	END IF;
END IF;

IF PageIndex<=0 THEN
	SET PageIndex=1;
END IF;
IF PageIndex>TotalPageAmount  THEN
  SET PageIndex = TotalPageAmount;
END IF;

SET StartRecord = (PageIndex-1)*PageSize + 1;
SET EndRecord = StartRecord + PageSize - 1;

SET @SqlCommand=CONCAT(@SqlCommand,') AS tempTb WHERE rowId BETWEEN ', CAST(StartRecord AS CHAR),' AND ',CAST(EndRecord AS CHAR));
SET @SqlCommand=CONCAT(@SqlCommand,' ORDER BY rowId');


PREPARE stmt FROM @SqlCommand;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

END;