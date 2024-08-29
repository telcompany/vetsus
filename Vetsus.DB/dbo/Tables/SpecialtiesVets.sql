CREATE TABLE [dbo].[SpecialtiesVets] (
    [Id]            VARCHAR (22)  NOT NULL,
    [SpecialtyId]   VARCHAR (22)  NOT NULL,
    [VetId]         VARCHAR (22)  NOT NULL,
    [PagingOrder]   INT           IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_SpecialtiesVets]             PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_SpecialtiesVets_PagingOrder] UNIQUE NONCLUSTERED ([PagingOrder] ASC),
    CONSTRAINT [FK_SpecialtiesVets_Specialties] FOREIGN KEY ([SpecialtyId]) REFERENCES [dbo].[Specialties] ([Id]),
    CONSTRAINT [FK_SpecialtiesVets_Vets]        FOREIGN KEY ([VetId]) REFERENCES [dbo].[Vets] ([Id])
);

