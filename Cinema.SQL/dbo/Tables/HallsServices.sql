CREATE TABLE [dbo].[HallsServices] (
    [HallId]    INT        NOT NULL,
    [ServiceId] INT        NOT NULL,
    [Price]     SMALLMONEY NOT NULL,
    CONSTRAINT [PK_HallsServices] PRIMARY KEY CLUSTERED ([HallId] ASC, [ServiceId] ASC),
    CONSTRAINT [FK_HallsServies_Halls] FOREIGN KEY ([HallId]) REFERENCES [dbo].[Halls] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_HallsServies_Services] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[Services] ([Id]) ON DELETE CASCADE
);

