CREATE TABLE [dbo].[ShowtimesDates] (
    [id]         BIGINT        NOT NULL,
    [showtimeId] BIGINT        NOT NULL,
    [Date]       DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_ShowtimeDate] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_ShowtimeDate_ShowtimeDate] FOREIGN KEY ([showtimeId]) REFERENCES [dbo].[Showtimes] ([Id])
);

