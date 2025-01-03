﻿CREATE PROCEDURE [dbo].[spGetUserRecords]
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
	  select COUNT(1) as Total from Users where IsDeleted = 0
	)
	select TOP ('+CONVERT(VARCHAR(7),@pageSize)+') u.Id, u.FirstName, u.LastName, u.Email, u.UserName, r.Name [Role], u.Created, u.CreatedBy, t.Total 
	from [dbo].[Users] u
	inner join UserRoles ur on u.Id = ur.UserId
	inner join Roles r on ur.RoleId = r.Id
	cross join total t
	where u.PagingOrder > @previousPagelastPageNumber
	and u.IsDeleted = 0
	order by u.PagingOrder
	';

	EXEC sp_executesql @sql,N'@previousPagelastPageNumber INT',@previousPagelastPageNumber;
END