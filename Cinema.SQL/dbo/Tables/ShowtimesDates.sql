CREATE TABLE [dbo].[ShowtimesDates] (
    [Id]         BIGINT        IDENTITY (1, 1) NOT NULL,
    [ShowtimeId] BIGINT        NOT NULL,
    [Date]       DATETIME2 (7) NOT NULL,
    CONSTRAINT [PK_ShowtimeDate] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ShowtimeDate_ShowtimeDate] FOREIGN KEY ([ShowtimeId]) REFERENCES [dbo].[Showtimes] ([Id])
);





