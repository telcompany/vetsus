CREATE TABLE [dbo].[Owners] (
    [Id]          VARCHAR (22)  NOT NULL,
    [FirstName]   VARCHAR (50)  NOT NULL,
    [LastName]    VARCHAR (50)  NOT NULL,
    [Address]     VARCHAR (100) NOT NULL,
    [Phone]       VARCHAR (10)  NULL,
    [Email]       VARCHAR (10)  NULL,
    [PagingOrder] INT           IDENTITY (1, 1) NOT NULL,
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Owners] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_Owners_PagingOrder] UNIQUE NONCLUSTERED ([PagingOrder] ASC),
    INDEX [IX_Owners_IsDeleted] NONCLUSTERED ([IsDeleted])
);

