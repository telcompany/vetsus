CREATE PROCEDURE [dbo].[spInsertUserRole]
    @userId VARCHAR(22),
    @roleId VARCHAR(22)
AS
BEGIN
    INSERT INTO [dbo].[UserRoles](UserId, RoleId)
	VALUES (@userId, @roleId)
END