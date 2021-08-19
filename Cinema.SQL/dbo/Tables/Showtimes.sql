CREATE TABLE [dbo].[Showtimes] (
    [Id]       BIGINT   NOT NULL,
    [Time]     TIME (7) NOT NULL,
    [Movie_Id] BIGINT   NOT NULL,
    [Hall_Id]  BIGINT   NOT NULL,
    CONSTRAINT [PK_Showtimes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Showtimes_Halls] FOREIGN KEY ([Hall_Id]) REFERENCES [dbo].[Halls] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Showtimes_Movies] FOREIGN KEY ([Movie_Id]) REFERENCES [dbo].[Movies] ([Id]) ON DELETE CASCADE
);

