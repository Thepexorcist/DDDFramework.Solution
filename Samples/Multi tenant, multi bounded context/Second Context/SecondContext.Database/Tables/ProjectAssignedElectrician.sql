CREATE TABLE [dbo].[ProjectAssignedElectrician]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[ProjectId] INT NOT NULL, 
    [EmployeeNumber] INT NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_ProjectAssignedElectrician_Project] FOREIGN KEY ([ProjectId]) REFERENCES [Project]([Id]),
)
