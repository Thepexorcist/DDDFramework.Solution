CREATE TABLE [dbo].[ProjectDocument]
(
	[Id] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [ProjectId] INT NOT NULL,
    [Name] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(50) NULL, 
    [URL] NVARCHAR(MAX) NOT NULL, 
    [Version] INT NOT NULL, 
    CONSTRAINT [FK_ProjectDocument_Project] FOREIGN KEY ([ProjectId]) REFERENCES [Project]([Id]) 
)
