CREATE TABLE [dbo].[Halls] (
    [Id]        BIGINT  IDENTITY (1, 1) NOT NULL,
    [Number]    TINYINT NOT NULL,
    [Cinema_Id] INT     NOT NULL,
    CONSTRAINT [PK_Halls] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Halls_Cinemas] FOREIGN KEY ([Cinema_Id]) REFERENCES [dbo].[Cinemas] ([Id]) ON DELETE CASCADE
);

