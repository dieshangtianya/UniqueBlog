use UniqueBlogDB;

DROP PROCEDURE IF EXISTS sp_get_items_super_pagination;

CREATE PROCEDURE sp_get_items_super_pagination
(
IN TableName VARCHAR(5000), #table name, it also support the table join
IN `Fields` VARCHAR(5000),  #fields want to query
IN SqlWhere VARCHAR(5000),  #where clause
IN GroupFields VARCHAR(5000), #group fields
IN OrderByFields VARCHAR(5000),#fields used to order
IN PageIndex INT, #current page index
IN PageSize INT, #items count every page can loaded
OUT TotalRecordAmount INT #output parameter, the total amount of the records
)
BEGIN

#declare two parameter to store the total page amount and sql which will be executed later
DECLARE TotalPageAmount INT;
DECLARE `Sql` TEXT;

DECLARE StartRecord INT;
DECLARE EndRecord INT;

IF SqlWhere IS NOT NULL AND LENGTH(SqlWhere)>0 AND LOCATE('WHERE',UPPER(LTRIM(SqlWhere)))!=1 THEN
	SET SqlWhere=' WHERE ' + SqlWhere;
END IF; 

IF GroupFields IS NOT NULL AND LENGTH(GroupFields)>0 AND LOCATE('GROUP BY',UPPER(LTRIM(GroupFields))) != 1 THEN
		SET GroupFields=' GROUP BY ' + GroupFields;
END IF;

IF OrderByFields IS NOT NULL AND LENGTH(OrderByFields)>0 AND LOCATE('ORDER BY',UPPER(LTRIM(OrderByFields)))!=1 THEN
		SET OrderByFields=' ORDER BY ' + OrderByFields;
END IF;

IF SqlWhere IS NULL OR SqlWhere='' THEN
	IF GroupFields IS NULL OR GroupFields='' THEN
		#if SqlWhere and GroupFields are both null or empty
		SET `Sql`='SELECT TotalRecordAmount = COUNT(*) FROM ' + TableName;
	ELSE
		#if SqlWhere is null but GroupFields is not null
		SET `Sql`='SELECT TotalRecordAmount = COUNT(*) FROM (SELECT ' + `Fields` + ' FROM ' + TableName + ' ' + GroupFields+') AS tempTb';
	END IF;
ELSE
	IF GroupFields IS NULL OR GroupFields='' THEN
		#if SqlWhere is not null or empty, but the GroupFields is null or empty
		SET `Sql`='SELECT TotalRecordAmount = COUNT(*) FROM ' + TableName + ' ' + SqlWhere;
	ELSE
		#if SqlWhere and GroupFields both are not null or empty
		SET `Sql`='SELECT TotalRecordAmount = COUNT(*) FROM (SELECT ' + `Fields` + ' FROM ' + TableName + ' ' + SqlWhere + ' '+GroupFields + ') AS tempTb';
	END IF;
END IF;

#execute the `Sql` to generate the total record amount
PREPARE stmt from @`Sql`;
EXECUTE stmt USING @TotalRecordAmount;
DEALLOCATE prepare stmt;

#Get the total page amount of it
SELECT TotalPageAmount=CEILING((TotalRecordAmount+0.0)/PageSize);

IF SqlWhere IS NULL OR SqlWhere='' THEN
	IF GroupFields IS NULL OR GroupFields='' THEN
		#SqlWhere and GroupFields are both null or empty
		SET `Sql`='SELECT * FROM (SELECT ROW_NUMBER() OVER ('+OrderByFields+') AS rowId,'+`Fields`+' FROM '+TableName;
	ELSE
		#SqlWhere is empty but the GroupFields is not
		SET `Sql`='SELECT * FROM (SELECT ROW_NUMBER() OVER ('+OrderByFields+') AS rowId,'+`Fields`+' FROM '+TableName+' '+GroupFields;
	END IF;
ELSE
	IF GroupFields IS NULL OR GroupFields='' THEN
			#SqlWhere is not null and the groupFields is null or empty
			SET `Sql`='SELECT * FROM (SELECT ROW_NUMBER() OVER (' + OrderByFields+') AS rowId,' + `Fields` + ' FROM ' + TableName+' ' + SqlWhere;
	ELSE
			#SqlWhere and GroupFields both aren't null or empty
			SET `Sql`='SELECT * FROM (SELECT ROW_NUMBER() OVER (' + OrderByFields+') AS rowId,' +`Fields` + ' FROM ' + TableName+' ' + SqlWhere+' ' + GroupFields;
	END IF;
END IF;

IF PageIndex<=0 THEN
	SET PageIndex = 1; 
END IF;  
IF PageIndex>TotalPageAmount THEN 
  SET PageIndex = TotalPageAmount;
END IF;

SET StartRecord = (PageIndex-1)*PageSize + 1;
SET EndRecord = StartRecord + PageSize - 1;

SET `Sql` = `Sql` + ') AS tempTb' + ' WHERE rowId BETWEEN ' + CONVERT(StartRecord,CHAR(50)) + ' AND ' +   CONVERT(EndRecord,CHAR(50));  
SET `Sql` = `Sql` + ' ORDER BY rowId OPTION(maxrecursion 0)';

PREPARE stmt2 from @`Sql`;
EXECUTE stmt2;
DEALLOCATE prepare stmt2;

END;