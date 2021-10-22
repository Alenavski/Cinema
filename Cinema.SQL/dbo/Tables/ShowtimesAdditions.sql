CREATE TABLE [dbo].[ShowtimesAdditions] (
    [ShowtimeId] BIGINT NOT NULL,
    [HallId]     INT    NOT NULL,
    [AdditionId] INT    NOT NULL,
    CONSTRAINT [PK_ShowtimesAdditions] PRIMARY KEY CLUSTERED ([ShowtimeId] ASC, [HallId] ASC, [AdditionId] ASC),
    CONSTRAINT [FK_ShowtimesAdditions_Additions] FOREIGN KEY ([AdditionId]) REFERENCES [dbo].[Additions] ([Id]),
    CONSTRAINT [FK_ShowtimesAdditions_Halls] FOREIGN KEY ([HallId]) REFERENCES [dbo].[Halls] ([Id]),
    CONSTRAINT [FK_ShowtimesAdditions_Showtimes] FOREIGN KEY ([ShowtimeId]) REFERENCES [dbo].[Showtimes] ([Id]) ON DELETE CASCADE
);



