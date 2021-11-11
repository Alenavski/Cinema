CREATE TABLE [dbo].[ShowtimesDates] (
    [Id]         BIGINT        NOT NULL,
    [ShowtimeId] BIGINT        NOT NULL,
    [Date]       DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_ShowtimeDate] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_ShowtimeDate_ShowtimeDate] FOREIGN KEY ([showtimeId]) REFERENCES [dbo].[Showtimes] ([Id])
);



