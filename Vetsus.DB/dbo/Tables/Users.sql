﻿CREATE TABLE [dbo].[Users]
(
	[Id] VARCHAR(22)  NOT NULL,
	[Email] VARCHAR (70) NOT NULL,
	[UserName] VARCHAR (50),
	[PasswordHash] VARCHAR(250),
	[PagingOrder] INT NOT NULL IDENTITY(1,1),
	CONSTRAINT [PK_Users_Id] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [UK_Users_PagingOrder] UNIQUE NONCLUSTERED (PagingOrder),
	CONSTRAINT [UK_Users_Email] UNIQUE NONCLUSTERED (Email)
)