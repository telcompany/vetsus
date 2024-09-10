CREATE PROCEDURE [dbo].[spGetUserById]
	@id VARCHAR(22)
AS
BEGIN
	select u.Id, u.Email, u.UserName, r.Name [Role]
	from [dbo].[Users] u
	inner join UserRoles ur on u.Id = ur.UserId
	inner join Roles r on ur.RoleId = r.Id
	where u.Id = @id
END