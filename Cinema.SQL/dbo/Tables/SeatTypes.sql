CREATE TABLE [dbo].[SeatTypes] (
    [Id]   TINYINT       IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (25) NOT NULL,
    CONSTRAINT [PK_SeatTypes] PRIMARY KEY CLUSTERED ([Id] ASC)
);

