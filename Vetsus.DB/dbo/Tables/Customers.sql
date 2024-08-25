CREATE TABLE [dbo].[Customers] (
    [Id]          VARCHAR (22)  NOT NULL,
    [FirstName]   VARCHAR (50)  NOT NULL,
    [LastName]    VARCHAR (50)  NOT NULL,
    [Address]     VARCHAR (100) NOT NULL,
    [Phone]       VARCHAR (10)  NULL,
    [Email]       VARCHAR (10)  NULL,
    [PagingOrder] INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_Customers_PagingOrder] UNIQUE NONCLUSTERED ([PagingOrder] ASC)
);

