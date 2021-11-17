CREATE TABLE [dbo].[TicketsAdditions] (
    [TicketId]   BIGINT  NOT NULL,
    [AdditionId] INT     NOT NULL,
    [Count]      TINYINT NOT NULL,
    CONSTRAINT [PK_TicketsAdditions] PRIMARY KEY CLUSTERED ([TicketId] ASC, [AdditionId] ASC),
    CONSTRAINT [FK_TicketsAdditions_Additions] FOREIGN KEY ([AdditionId]) REFERENCES [dbo].[Additions] ([Id]),
    CONSTRAINT [FK_TicketsAdditions_Tickets] FOREIGN KEY ([TicketId]) REFERENCES [dbo].[Tickets] ([Id]) ON DELETE CASCADE
);



