CREATE TABLE [dbo].[Tickets] (
    [Id]             BIGINT        IDENTITY (1, 1) NOT NULL,
    [DateOfBooking]  DATETIME2 (7) NOT NULL,
    [ShowtimeDateId] BIGINT        NOT NULL,
    [UserId]         INT           NOT NULL,
    [IsOrdered]      BIT           NOT NULL,
    CONSTRAINT [PK_Tickets] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Tickets_Showtimes] FOREIGN KEY ([ShowtimeDateId]) REFERENCES [dbo].[ShowtimesDates] ([Id]),
    CONSTRAINT [FK_Tickets_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([Id]) ON DELETE CASCADE
);


