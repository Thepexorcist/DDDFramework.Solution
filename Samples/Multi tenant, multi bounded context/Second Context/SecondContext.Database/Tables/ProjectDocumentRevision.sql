CREATE TABLE [dbo].[ProjectDocumentRevision]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[DocumentId] UNIQUEIDENTIFIER NOT NULL,
	[Comment] NVARCHAR(50) NOT NULL, 
    [Version] INT NOT NULL, 
    CONSTRAINT [FK_ProjectDocumentRevision_Document] FOREIGN KEY ([DocumentId]) REFERENCES [ProjectDocument]([Id])
)
