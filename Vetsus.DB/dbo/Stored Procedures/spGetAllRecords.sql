CREATE PROCEDURE [dbo].[spGetAllRecords]
	@tableName VARCHAR(50),
	@columns VARCHAR(MAX) = NULL,
	@pageNumber INT = 1,
	@pageSize INT = 100
AS
BEGIN
	DECLARE @sql NVARCHAR(MAX);	
	DECLARE @previousPagelastPageNumber INT;

	SET @previousPagelastPageNumber = (@pageNumber-1)*@pageSize;

	IF @columns IS NULL
	   SET @columns = '*';

	SET @sql = N'
	WITH total AS
	(
	  SELECT COUNT(1) AS Total FROM '+ QUOTENAME(@tableName) +' WHERE IsDeleted = 0
	)
	select TOP ('+CONVERT(VARCHAR(7),@pageSize)+') ' + @columns + '
	from ' + QUOTENAME(@tableName) + '
	cross join total
	where PagingOrder > @previousPagelastPageNumber
	and IsDeleted = 0
	order by PagingOrder
	';

	EXEC sp_executesql @sql,N'@previousPagelastPageNumber INT',@previousPagelastPageNumber;
END