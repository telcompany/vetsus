CREATE TABLE [dbo].[Species] (
    [Id]          VARCHAR (22)  NOT NULL,
    [Name]        VARCHAR (50)  NOT NULL,
    [PagingOrder] INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_Species] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_Species_PagingOrder] UNIQUE NONCLUSTERED ([PagingOrder] ASC)
);

