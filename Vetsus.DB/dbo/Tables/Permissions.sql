CREATE TABLE [dbo].[Permissions]
(
	[Id] VARCHAR(22)  NOT NULL,
	[Name] VARCHAR(50)  NOT NULL,
	[PagingOrder] INT NOT NULL IDENTITY(1,1),
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [PK_Permissions_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [UK_Permissions_PagingOrder] UNIQUE NONCLUSTERED (PagingOrder),
	INDEX [IX_Permissions_Name] NONCLUSTERED (Name) WITH (FILLFACTOR=100),
	INDEX [IX_Permissions_IsDeleted] NONCLUSTERED ([IsDeleted])
)