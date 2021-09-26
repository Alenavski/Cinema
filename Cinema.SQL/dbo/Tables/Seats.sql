CREATE TABLE [dbo].[Seats] (
    [Id]         BIGINT  IDENTITY (1, 1) NOT NULL,
    [Index]      TINYINT NOT NULL,
    [Row]        TINYINT NOT NULL,
    [Place]      TINYINT NOT NULL,
    [SeatTypeId] TINYINT NOT NULL,
    [HallId]     INT     NOT NULL,
    CONSTRAINT [PK_Seats] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Seats_Halls] FOREIGN KEY ([HallId]) REFERENCES [dbo].[Halls] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE,
    CONSTRAINT [FK_Seats_SeatTypes] FOREIGN KEY ([SeatTypeId]) REFERENCES [dbo].[SeatTypes] ([Id]) ON DELETE CASCADE ON UPDATE CASCADE
);



