CREATE TABLE [dbo].[Cinemas] (
    [Id]      INT             IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (50)   NOT NULL,
    [City]    NVARCHAR (50)   NOT NULL,
    [Address] NVARCHAR (50)   NOT NULL,
    [Image]   VARBINARY (MAX) NULL,
    CONSTRAINT [PK_Cinemas] PRIMARY KEY CLUSTERED ([Id] ASC)
);

