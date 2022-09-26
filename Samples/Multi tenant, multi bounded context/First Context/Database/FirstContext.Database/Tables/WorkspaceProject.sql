CREATE TABLE [dbo].[WorkspaceProject]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [WorkspaceId] INT NOT NULL,
    [UniqueProjectNumber] NVARCHAR(MAX) NOT NULL, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [Created] DATETIME NOT NULL, 
    [IsPublished] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_WorkspaceProject_Workspace] FOREIGN KEY ([WorkspaceId]) REFERENCES [Workspace]([Id])
)
