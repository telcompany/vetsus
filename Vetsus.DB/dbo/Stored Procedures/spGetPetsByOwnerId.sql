CREATE PROCEDURE [dbo].[spGetPetsByOwnerId]
@ownerId VARCHAR(22)
AS
BEGIN
	select p.Id as PetId, 
	p.Name, 
	CASE
		WHEN p.Gender = 'M' THEN 'Masculino'
		ELSE 'Femenino'
	END AS Gender,
	s.Name as Species, 
	CASE
		WHEN p.BirthDate is null THEN '-'
		ELSE CONVERT(varchar, p.BirthDate, 3)
	END AS BirthDate
	from Pets p
	inner join Species s on p.SpeciesId = s.Id
	where p.OwnerId = @ownerId and p.IsDeleted = 0
	order by p.Created desc
END

