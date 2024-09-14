CREATE TABLE [dbo].[Specialties] (
    [Id]          VARCHAR (22)  NOT NULL,
    [Name]        VARCHAR (50)  NOT NULL,
    [PagingOrder] INT           IDENTITY (1, 1) NOT NULL,
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Specialties] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_Specialties_PagingOrder] UNIQUE NONCLUSTERED ([PagingOrder] ASC),
    INDEX [IX_Specialties_IsDeleted] NONCLUSTERED ([IsDeleted])
);

