CREATE TABLE [dbo].[RolePermissions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[RoleId] VARCHAR(22)  NOT NULL,	
	[PermissionId] VARCHAR(22)  NOT NULL,
	CONSTRAINT [FK_RolePermissions_Permissions] FOREIGN KEY ([PermissionId]) REFERENCES [dbo].[Permissions] ([Id]),
	CONSTRAINT [FK_RolePermissions_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([Id])
)
