CREATE TABLE [dbo].[WorkspaceLogEntry]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
    [WorkspaceId] INT NOT NULL, 
    [Created] DATETIME NOT NULL, 
    [Action] NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT [FK_WorkspaceLogEntry_Workspace] FOREIGN KEY ([WorkspaceId]) REFERENCES [Workspace]([Id])
)
