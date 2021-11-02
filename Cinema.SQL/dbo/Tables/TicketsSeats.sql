CREATE TABLE [dbo].[TicketsSeats] (
    [TicketId]  BIGINT NOT NULL,
    [SeatId]    BIGINT NOT NULL,
    [IsOrdered] BIT    NULL,
    CONSTRAINT [PK_TicketsSeats] PRIMARY KEY CLUSTERED ([TicketId] ASC, [SeatId] ASC),
    CONSTRAINT [FK_TicketsSeats_Seats] FOREIGN KEY ([SeatId]) REFERENCES [dbo].[Seats] ([Id]),
    CONSTRAINT [FK_TicketsSeats_Tickets] FOREIGN KEY ([TicketId]) REFERENCES [dbo].[Tickets] ([Id]) ON DELETE CASCADE
);

