CREATE TABLE [dbo].[TicketsPrices] (
    [ShowtimeId] BIGINT          NOT NULL,
    [SeatTypeId] SMALLINT        NOT NULL,
    [Price]      DECIMAL (18, 4) NOT NULL,
    CONSTRAINT [PK_ShowtimesSeatTypes] PRIMARY KEY CLUSTERED ([SeatTypeId] ASC, [ShowtimeId] ASC),
    CONSTRAINT [FK_ShowtimesSeatTypes_SeatTypes] FOREIGN KEY ([SeatTypeId]) REFERENCES [dbo].[SeatTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_ShowtimesSeatTypes_Showtimes] FOREIGN KEY ([ShowtimeId]) REFERENCES [dbo].[Showtimes] ([Id]) ON DELETE CASCADE
);