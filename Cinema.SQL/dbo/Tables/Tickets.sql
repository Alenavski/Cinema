CREATE TABLE [dbo].[Tickets] (
    [Id]            BIGINT        IDENTITY (1, 1) NOT NULL,
    [DateOfBooking] DATETIME2 (7) NOT NULL,
    [ShowtimeId]    BIGINT        NOT NULL,
    [UserId]        INT           NOT NULL,
    CONSTRAINT [PK_Tickets] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tickets_Showtimes] FOREIGN KEY ([ShowtimeId]) REFERENCES [dbo].[Showtimes] ([Id]),
    CONSTRAINT [FK_Tickets_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);



