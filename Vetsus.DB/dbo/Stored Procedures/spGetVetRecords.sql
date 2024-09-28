CREATE PROCEDURE [dbo].[spGetVetRecords]
	@pageNumber INT = 1,
	@pageSize INT = 100
AS
BEGIN
	DECLARE @sql NVARCHAR(MAX);	
	DECLARE @previousPagelastPageNumber INT;

	SET @previousPagelastPageNumber = (@pageNumber-1)*@pageSize;

	SET @sql = N'
	WITH total AS
	(
	  SELECT COUNT(1) AS Total FROM Vets WHERE IsDeleted = 0
	)
	select TOP ('+CONVERT(VARCHAR(7),@pageSize)+') v.Id, v.FirstName, v.LastName, v.Phone, t.Total 
	from Vets v
	cross join total t
	where v.PagingOrder > @previousPagelastPageNumber
	and v.IsDeleted = 0
	order by v.PagingOrder
	';

	EXEC sp_executesql @sql,N'@previousPagelastPageNumber INT',@previousPagelastPageNumber;
END