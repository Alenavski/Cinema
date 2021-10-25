CREATE TABLE [dbo].[ShowtimesAdditions] (
    [ShowtimeId] BIGINT NOT NULL,
    [AdditionId] INT    NOT NULL,
    CONSTRAINT [FK_ShowtimesAdditions_Additions] FOREIGN KEY ([AdditionId]) REFERENCES [dbo].[Additions] ([Id]),
    CONSTRAINT [FK_ShowtimesAdditions_Showtimes] FOREIGN KEY ([ShowtimeId]) REFERENCES [dbo].[Showtimes] ([Id]) ON DELETE CASCADE
);