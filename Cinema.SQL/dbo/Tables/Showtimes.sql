CREATE TABLE [dbo].[Showtimes] (
    [Id]      BIGINT   IDENTITY (1, 1) NOT NULL,
    [Time]    DATETIME2 (7) NOT NULL,
    [NumberOfFreeSeats] SMALLINT NULL,
    [MovieId] INT      NOT NULL,
    [HallId]  INT      NOT NULL,
    CONSTRAINT [PK_Showtimes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Showtimes_Halls] FOREIGN KEY ([HallId]) REFERENCES [dbo].[Halls] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Showtimes_Movies] FOREIGN KEY ([MovieId]) REFERENCES [dbo].[Movies] ([Id]) ON DELETE CASCADE
);



