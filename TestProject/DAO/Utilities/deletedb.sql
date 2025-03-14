-- Eliminar la base de datos AutoImperial si existe
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'AutoImperial')
BEGIN
    ALTER DATABASE [AutoImperial] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [AutoImperial];
END
GO
