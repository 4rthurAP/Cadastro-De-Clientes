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
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210527141552_Start')
BEGIN
    CREATE TABLE [Clientes] (
        [Id_Cliente] int NOT NULL IDENTITY,
        [Nivel_De_Acesso] int NOT NULL,
        [CNPJ] nvarchar(max) NULL,
        [Razao_Social] nvarchar(max) NULL,
        [Nome_Fantasia] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [Telefone] nvarchar(max) NULL,
        [Senha] nvarchar(max) NULL,
        [DataCreate] datetime2 NOT NULL,
        [DataUpdate] datetime2 NOT NULL,
        CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id_Cliente])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20210527141552_Start')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20210527141552_Start', N'5.0.6');
END;
GO

COMMIT;
GO

