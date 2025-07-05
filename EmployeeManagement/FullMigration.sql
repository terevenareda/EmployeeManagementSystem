IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Departments] (
    [ID] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Departments] PRIMARY KEY ([ID])
);

CREATE TABLE [LogHistories] (
    [ID] int NOT NULL IDENTITY,
    [Action] nvarchar(max) NOT NULL,
    [EmployeeId] int NOT NULL,
    [EmployeeName] nvarchar(max) NOT NULL,
    [Timestamp] datetime2 NOT NULL,
    CONSTRAINT [PK_LogHistories] PRIMARY KEY ([ID])
);

CREATE TABLE [Employees] (
    [ID] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [DepartmentId] int NOT NULL,
    [HireDate] datetime2 NOT NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Employees_Departments_DepartmentId] FOREIGN KEY ([DepartmentId]) REFERENCES [Departments] ([ID]) ON DELETE CASCADE
);

CREATE INDEX [IX_Employees_DepartmentId] ON [Employees] ([DepartmentId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250705180458_InitialCreate', N'9.0.6');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250705191334_AddLogHistory', N'9.0.6');

DECLARE @var sysname;
SELECT @var = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Employees]') AND [c].[name] = N'Email');
IF @var IS NOT NULL EXEC(N'ALTER TABLE [Employees] DROP CONSTRAINT [' + @var + '];');
ALTER TABLE [Employees] ALTER COLUMN [Email] nvarchar(450) NOT NULL;

CREATE UNIQUE INDEX [IX_Employees_Email] ON [Employees] ([Email]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250705200215_AddUniqueEmailToEmployee', N'9.0.6');

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250705200501_AddEmailUniqueConstraint', N'9.0.6');

COMMIT;
GO

