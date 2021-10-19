CREATE TABLE [dbo].[HallsAdditions] (
    [HallId]     INT             NOT NULL,
    [AdditionId] INT             NOT NULL,
    [Price]      DECIMAL (18, 4) NOT NULL,
    CONSTRAINT [PK_HallsServices] PRIMARY KEY CLUSTERED ([HallId] ASC, [AdditionId] ASC),
    CONSTRAINT [FK_HallsServies_Halls] FOREIGN KEY ([HallId]) REFERENCES [dbo].[Halls] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_HallsServies_Services] FOREIGN KEY ([AdditionId]) REFERENCES [dbo].[Additions] ([Id]) ON DELETE CASCADE
);

