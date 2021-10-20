CREATE TABLE [dbo].[TicketsAdditions] (
    [TicketShowtimeId] BIGINT   NOT NULL,
    [TicketSeatTypeId] SMALLINT NOT NULL,
    [HallId]           INT      NOT NULL,
    [AdditionId]       INT      NOT NULL,
    [Count]            TINYINT  NOT NULL,
    CONSTRAINT [PK_TicketsAdditions] PRIMARY KEY CLUSTERED ([TicketShowtimeId] ASC, [AdditionId] ASC, [HallId] ASC, [TicketSeatTypeId] ASC),
    CONSTRAINT [FK_TicketsAdditions_HallsAdditions] FOREIGN KEY ([HallId], [AdditionId]) REFERENCES [dbo].[HallsAdditions] ([HallId], [AdditionId]) ON DELETE CASCADE
);

