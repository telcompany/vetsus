﻿CREATE TABLE [dbo].[Roles]
(
	[Id] VARCHAR(22)  NOT NULL,
	[Name] VARCHAR(50)  NOT NULL,
	[PagingOrder] INT NOT NULL IDENTITY(1,1),
	[IsDeleted] BIT NOT NULL DEFAULT 0,
	CONSTRAINT [PK_Roles_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [UK_Roles_PagingOrder] UNIQUE NONCLUSTERED (PagingOrder),
	INDEX [IX_Roles_Name] NONCLUSTERED (Name) WITH (FILLFACTOR=100),
	INDEX [IX_Roles_IsDeleted] NONCLUSTERED ([IsDeleted])
)