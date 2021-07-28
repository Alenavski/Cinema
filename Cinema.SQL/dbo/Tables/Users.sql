CREATE TABLE [dbo].[Users] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Email]    NVARCHAR (254) NOT NULL,
    [Password] NVARCHAR (64) NOT NULL,
    [Salt]     NCHAR (16) NOT NULL,
    [Role]     NCHAR (10) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([Id] ASC)
);




GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Users_Email]
    ON [dbo].[Users]([Email] ASC);

