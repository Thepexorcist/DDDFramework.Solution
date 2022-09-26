CREATE TABLE [dbo].[ProjectAssignedProjectManager]
(
	[ProjectId] INT NOT NULL PRIMARY KEY, 
	[EmployeeNumber] INT NOT NULL, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_ProjectAssignedProjectManager_Project] FOREIGN KEY ([ProjectId]) REFERENCES [Project]([Id])
)
