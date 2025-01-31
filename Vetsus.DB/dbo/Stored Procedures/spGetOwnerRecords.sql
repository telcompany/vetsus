CREATE PROCEDURE [dbo].[spGetOwnerRecords]
@pageNumber INT = 1,
@pageSize INT = 100
AS
BEGIN
	DECLARE @previousPagelastPageNumber INT;

	SET @previousPagelastPageNumber = (@pageNumber-1)*@pageSize;
	
	with total as
	(
		select COUNT(1) as Total from Owners where IsDeleted = 0
	),
	pagedRecords as 
	(
		select top(@pageSize)
		o.Id, 
		o.FirstName, 
		o.LastName,
		o.Phone,
		o.Created, 
		o.CreatedBy 
		from Owners o
		where PagingOrder > @previousPagelastPageNumber
		and IsDeleted = 0
		order by PagingOrder
	),
	query as 
	(
		select o.*, COUNT(p.Id) as TotalPets
		from pagedRecords o
		left join Pets p on o.Id = p.OwnerId
		group by o.Id, o.FirstName, o.LastName, o.Phone, o.Created, o.CreatedBy
	)

	select q.*, total from query q 
	cross join total

END

