CREATE TABLE [dbo].[Pets] (
    [Id]          VARCHAR (22)  NOT NULL,
    [Name]        VARCHAR (50)  NOT NULL,
    [BirthDate]   DATETIME      NULL,
    [SpeciesId]   VARCHAR (22)  NOT NULL,
    [OwnerId]     VARCHAR (22)  NOT NULL,
    [PagingOrder] INT           IDENTITY (1, 1) NOT NULL,
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    CONSTRAINT [PK_Pets] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_Pets_PagingOrder] UNIQUE NONCLUSTERED ([PagingOrder] ASC),
    CONSTRAINT [FK_Pets_Owners] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[Owners] ([Id]),
    CONSTRAINT [FK_Pets_Species] FOREIGN KEY ([SpeciesId]) REFERENCES [dbo].[Species] ([Id]),
    INDEX [IX_Pets_IsDeleted] NONCLUSTERED ([IsDeleted])
);

