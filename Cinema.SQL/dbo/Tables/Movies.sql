CREATE TABLE [dbo].[Movies] (
    [Id]          INT             IDENTITY (1, 1) NOT NULL,
    [Title]       NVARCHAR (150)  NOT NULL,
    [Description] NVARCHAR (1000) NOT NULL,
    [Poster]      VARBINARY (MAX) NULL,
    [StartDate]   DATETIME2 (7)   NOT NULL,
    [EndDate]     DATETIME2 (7)   NOT NULL,
    CONSTRAINT [PK_Movies] PRIMARY KEY CLUSTERED ([Id] ASC)
);



