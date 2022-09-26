CREATE TABLE [dbo].[Project]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [TenantId] INT NOT NULL, 
    [ProjectNumber] NVARCHAR(50) NOT NULL, 
    [DisplayName] NVARCHAR(50) NOT NULL, 
    [IsPublished] BIT NOT NULL DEFAULT 0
)
