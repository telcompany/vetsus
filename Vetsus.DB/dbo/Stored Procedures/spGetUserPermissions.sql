CREATE PROCEDURE [dbo].[spGetUserPermissions]
	@userId VARCHAR(22)
AS
BEGIN
	SELECT DISTINCT p.* FROM [Permissions] as p
	INNER JOIN RolePermissions as rp
	ON p.Id= rp.PermissionId
	INNER JOIN Roles as r
	ON r.Id = rp.RoleId
	INNER JOIN UserRoles as ur
	ON r.Id=ur.RoleId
	INNER JOIN Users as u
	ON ur.UserId=u.Id
	WHERE u.Id=@userId
END
