CREATE TABLE [dbo].[Vets] (
    [Id]          VARCHAR (22)  NOT NULL,
    [FirstName]   VARCHAR (50)  NOT NULL,
    [LastName]    VARCHAR (50)  NOT NULL,
    [Phone]       VARCHAR (10)  NULL,
    [PagingOrder] INT           IDENTITY (1, 1) NOT NULL,
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Vets] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_Vets_PagingOrder] UNIQUE NONCLUSTERED ([PagingOrder] ASC),
    INDEX [IX_Vets_IsDeleted] NONCLUSTERED ([IsDeleted])
);

