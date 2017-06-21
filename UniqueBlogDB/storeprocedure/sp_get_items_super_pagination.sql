use UniqueBlogDB;

IF EXISTS (SELECT * 
			FROM sysobjects
			WHERE name='sp_get_items_super_pagination' AND xtype='p')
DROP PROCEDURE sp_get_items_super_pagination
GO

CREATE PROCEDURE sp_get_items_super_pagination
(
@TableName varchar(5000), --table name, it also support the table join
@Fields varchar(5000), --fields want to query
@SqlWhere varchar(5000), --where clause
@GroupFields varchar(5000), --group fields
@OrderByFields varchar(5000),--fields used to order
@PageIndex int, --current page index
@PageSize int, --items count every page can loaded
@TotalRecordAmount int OUTPUT --output parameter, the total amount of the records
)
AS
BEGIN
	IF(@SqlWhere IS NOT NULL AND LEN(@SqlWhere)>0 AND CHARINDEX('WHERE', UPPER(LTRIM(@SqlWhere)),1)!=1)
		SET @SqlWhere=' WHERE '+@SqlWhere

	IF(@GroupFields IS NOT NULL AND LEN(@GroupFields)>0 AND CHARINDEX('GROUP BY',UPPER(LTRIM(@GroupFields)),1) != 1)
		SET @GroupFields=' GROUP BY '+@GroupFields

	IF(@OrderByFields IS NOT NULL AND LEN(@OrderByFields)>0 AND CHARINDEX('ORDER BY',UPPER(LTRIM(@OrderByFields)),1)!=1)
		SET @OrderByFields=' ORDER BY '+@OrderByFields

	DECLARE @TotalPageAmount int
	DECLARE @Sql nvarchar(MAX)
	
	IF(@SqlWhere IS NULL OR @SqlWhere='')
		IF(@GroupFields IS NULL OR @GroupFields='')
			--if @SqlWhere and @GroupFields are both null or empty
			SET @Sql='SELECT @TotalRecordAmount = COUNT(*) FROM '+ @TableName + @SqlWhere
		ELSE
			--if @SqlWhere is null but @GroupFields is not null
			SET @Sql='SELECT @TotalRecordAmount = COUNT(*) FROM (SELECT ' + @Fields + ' FROM ' + @TableName + ' ' + @GroupFields+') AS tempTb'
	ELSE
		IF(@GroupFields IS NULL OR @GroupFields='')
			SET @Sql='SELECT @TotalRecordAmount = COUNT(*) FROM '+ @TableName + ' ' + @SqlWhere
		ELSE
			SET @Sql='SELECT @TotalRecordAmount = COUNT(*) FROM (SELECT ' + @Fields + ' FROM ' + @TableName + ' ' + @SqlWhere + ' '+@GroupFields + ') AS tempTb'
		
	--execute dynamic sql with input or output parameter
	EXEC sp_executesql @Sql,N'@TotalRecordAmount int OUTPUT', @TotalRecordAmount OUTPUT

	--get the total page
	SELECT @TotalPageAmount=CEILING((@TotalRecordAmount+0.0)/@PageSize)

	IF(@SqlWhere IS NULL OR @SqlWhere='')
		IF(@GroupFields IS NULL OR @GroupFields='')
			SET @Sql='SELECT * FROM (SELECT ROW_NUMBER() OVER ('+@OrderByFields+') AS rowId,'+@Fields+' FROM '+@TableName
		ELSE
			SET @Sql='SELECT * FROM (SELECT ROW_NUMBER() OVER ('+@OrderByFields+') AS rowId,'+@Fields+' FROM '+@TableName+' '+@GroupFields
	ELSE
		IF(@GroupFields IS NULL OR @GroupFields='')
			SET @Sql='SELECT * FROM (SELECT ROW_NUMBER() OVER ('+@OrderByFields+') AS rowId,'+@Fields+' FROM '+@TableName+' '+@SqlWhere
		ELSE
			SET @Sql='SELECT * FROM (SELECT ROW_NUMBER() OVER ('+@OrderByFields+') AS rowId,'+@Fields+' FROM '+@TableName+' '+@SqlWhere+' '+@GroupFields


	IF @PageIndex<=0   
       SET @PageIndex = 1  
    IF @PageIndex>@TotalPageAmount  
       SET @PageIndex = @TotalPageAmount  

	DECLARE @StartRecord int
    DECLARE @EndRecord int
    SET @StartRecord = (@PageIndex-1)*@PageSize + 1 
    SET @EndRecord = @StartRecord + @pageSize - 1

	SET @Sql = @Sql + ') AS tempTb' + ' WHERE rowId BETWEEN ' + CONVERT(varchar(50),@StartRecord) + ' AND ' +   CONVERT(varchar(50),@EndRecord)  
    SET @sql = @Sql + ' ORDER BY rowId OPTION(maxrecursion 0)'
	
	--execute the sql to get the result
	Exec(@Sql)  
END