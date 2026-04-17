-- =============================================
-- Script de Creación de Base de Datos y Objetos
-- Proyecto: Prueba Técnica Analista Programador
-- =============================================

USE master;
GO

-- 1. Crear la Base de Datos
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'TEST_JARDINES')
BEGIN
    CREATE DATABASE TEST_JARDINES;
END
GO

USE TEST_JARDINES;
GO

-- 2. Crear la Tabla Productos
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Productos')
BEGIN
    CREATE TABLE Productos (
        Id INT PRIMARY KEY IDENTITY(1,1),
        Nombre VARCHAR(100) NOT NULL,
        Precio DECIMAL(10,2) NOT NULL,
        Stock INT NOT NULL
    );
END
GO

-- 3. Procedimiento para Consultar
CREATE OR ALTER PROCEDURE sp_ConsultarProductos
AS
BEGIN
    SELECT Id, Nombre, Precio, Stock FROM Productos;
END;
GO

-- 4. Procedimiento para Insertar
CREATE OR ALTER PROCEDURE sp_InsertarProducto
    @Nombre VARCHAR(100),
    @Precio DECIMAL(10,2),
    @Stock INT
AS
BEGIN
    INSERT INTO Productos (Nombre, Precio, Stock)
    VALUES (@Nombre, @Precio, @Stock);
END;
GO

-- 5. Procedimiento para Actualizar
CREATE OR ALTER PROCEDURE sp_ActualizarProducto
    @Id INT,
    @Nombre VARCHAR(100),
    @Precio DECIMAL(10,2),
    @Stock INT
AS
BEGIN
    UPDATE Productos 
    SET Nombre = @Nombre, 
        Precio = @Precio, 
        Stock = @Stock 
    WHERE Id = @Id;
END;
GO

-- 6. Procedimiento para Eliminar
CREATE OR ALTER PROCEDURE sp_EliminarProducto
    @Id INT
AS
BEGIN
    DELETE FROM Productos WHERE Id = @Id;
END;
GO

-- 7. Crear el Usuario 'prueba' y asignar permisos
-- Nota: Asegúrese de que el servidor tenga activada la Autenticación Mixta
IF NOT EXISTS (SELECT * FROM sys.server_principals WHERE name = 'prueba')
BEGIN
    CREATE LOGIN [prueba] WITH PASSWORD = N'Jardines**2026', DEFAULT_DATABASE = [TEST_JARDINES], CHECK_EXPIRATION = OFF, CHECK_POLICY = OFF;
END
GO

USE TEST_JARDINES;
GO
IF NOT EXISTS (SELECT * FROM sys.database_principals WHERE name = 'prueba')
BEGIN
    CREATE USER [prueba] FOR LOGIN [prueba];
    EXEC sp_addrolemember 'db_owner', 'prueba';
END
GO
