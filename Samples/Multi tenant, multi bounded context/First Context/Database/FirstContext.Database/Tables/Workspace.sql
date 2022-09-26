CREATE TABLE [dbo].[Workspace]
(
	[Id] INT NOT NULL PRIMARY KEY,
    [TenantId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [IsActive] BIT NOT NULL, 
    CONSTRAINT [FK_Workspace_Tenant] FOREIGN KEY ([TenantId]) REFERENCES [Tenant]([Id])
)
