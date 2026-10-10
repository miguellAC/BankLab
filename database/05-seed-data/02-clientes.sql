-- 02-clientes.sql
-- Generates 2,000 clients, with addresses and contacts
-- Approx: 2000 Clientes, 3000 DireccionesCliente, 2500 ContactosCliente

SET NOCOUNT ON;
GO

PRINT 'Poblando tablas de clientes...';

-- Generate 2000 Clientes
IF OBJECT_ID('tempdb..#Nombres') IS NOT NULL DROP TABLE #Nombres;
CREATE TABLE #Nombres (Id INT IDENTITY(1,1), Nombre VARCHAR(50));
INSERT INTO #Nombres (Nombre) VALUES 
('Juan'), ('Carlos'), ('Luis'), ('Andres'), ('Diego'), ('Jorge'), ('Pedro'), ('Jose'), ('David'), ('Daniel'),
('Miguel'), ('Alejandro'), ('Camilo'), ('Fernando'), ('Oscar'), ('Hector'), ('Ivan'), ('Ricardo'), ('Mauricio'), ('Javier'),
('Maria'), ('Ana'), ('Diana'), ('Laura'), ('Carolina'), ('Marta'), ('Andrea'), ('Paula'), ('Sandra'), ('Patricia'),
('Claudia'), ('Natalia'), ('Gloria'), ('Luz'), ('Adriana'), ('Marcela'), ('Paola'), ('Johana'), ('Monica'), ('Angela'),
('Victor'), ('Mario'), ('Gabriel'), ('Julian'), ('Eduardo'), ('Isabel'), ('Teresa'), ('Rosa'), ('Carmen'), ('Beatriz');

IF OBJECT_ID('tempdb..#Apellidos') IS NOT NULL DROP TABLE #Apellidos;
CREATE TABLE #Apellidos (Id INT IDENTITY(1,1), Apellido VARCHAR(50));
INSERT INTO #Apellidos (Apellido) VALUES 
('Rodriguez'), ('Gomez'), ('Gonzalez'), ('Martinez'), ('Garcia'), ('Lopez'), ('Hernandez'), ('Perez'), ('Sanchez'), ('Ramirez'),
('Diaz'), ('Munoz'), ('Rojas'), ('Moreno'), ('Jimenez'), ('Gutierrez'), ('Ortiz'), ('Vargas'), ('Castro'), ('Suarez'),
('Silva'), ('Romero'), ('Guzman'), ('Herrera'), ('Reyes'), ('Mendoza'), ('Aguilar'), ('Acosta'), ('Medina'), ('Rios'),
('Castillo'), ('Franco'), ('Navarro'), ('Montoya'), ('Cortes'), ('Gallego'), ('Marin'), ('Jaramillo'), ('Mejia'), ('Osorio');

SET IDENTITY_INSERT Clientes ON;
WITH Nums AS (
    SELECT TOP (2000) ROW_NUMBER() OVER(ORDER BY (SELECT NULL)) AS N
    FROM master.dbo.spt_values a CROSS JOIN master.dbo.spt_values b
)
INSERT INTO Clientes (Id, SucursalId, TipoIdentificacionId, Identificacion, Nombres, Apellidos, FechaNacimiento, Estado, FechaRegistro)
SELECT 
    N AS Id,
    (ABS(CHECKSUM(NEWID())) % 10) + 1 AS SucursalId,
    CASE 
        WHEN N % 100 < 80 THEN 1 -- CC (80%)
        WHEN N % 100 < 90 THEN 3 -- CE (10%)
        WHEN N % 100 < 95 THEN 2 -- NIT (5%)
        WHEN N % 100 < 98 THEN 4 -- TI (3%)
        ELSE 5 -- PA (2%)
    END AS TipoIdentificacionId,
    CAST(1000000000 + N AS VARCHAR) AS Identificacion,
    (SELECT Nombre FROM #Nombres WHERE Id = (ABS(CHECKSUM(NEWID())) % 50) + 1) + 
        CASE WHEN N % 2 = 0 THEN ' ' + (SELECT Nombre FROM #Nombres WHERE Id = (ABS(CHECKSUM(NEWID())) % 50) + 1) ELSE '' END AS Nombres,
    (SELECT Apellido FROM #Apellidos WHERE Id = (ABS(CHECKSUM(NEWID())) % 40) + 1) + ' ' +
        (SELECT Apellido FROM #Apellidos WHERE Id = (ABS(CHECKSUM(NEWID())) % 40) + 1) AS Apellidos,
    DATEADD(DAY, -(ABS(CHECKSUM(NEWID())) % 20000) - 6570, GETDATE()) AS FechaNacimiento, -- 18 to 72 years ago
    CASE 
        WHEN N % 100 < 90 THEN 'ACTIVO'
        WHEN N % 100 < 95 THEN 'INACTIVO'
        WHEN N % 100 < 98 THEN 'BLOQUEADO'
        ELSE 'FALLECIDO'
    END AS Estado,
    DATEADD(DAY, -(ABS(CHECKSUM(NEWID())) % 3000), GETDATE()) AS FechaRegistro
FROM Nums;
SET IDENTITY_INSERT Clientes OFF;

-- DireccionesCliente
INSERT INTO DireccionesCliente (ClienteId, TipoDireccion, Direccion, Ciudad, Departamento, Pais, EsPrincipal)
SELECT 
    Id AS ClienteId,
    'RESIDENCIAL' AS TipoDireccion,
    'Calle ' + CAST(ABS(CHECKSUM(NEWID())) % 150 + 1 AS VARCHAR) + ' # ' + CAST(ABS(CHECKSUM(NEWID())) % 100 + 1 AS VARCHAR) + '-' + CAST(ABS(CHECKSUM(NEWID())) % 100 + 1 AS VARCHAR) AS Direccion,
    CHOOSE((ABS(CHECKSUM(NEWID())) % 10) + 1, 'Bogotá', 'Medellín', 'Cali', 'Barranquilla', 'Cartagena', 'Bucaramanga', 'Pereira', 'Manizales', 'Ibagué', 'Santa Marta') AS Ciudad,
    'Dpto' AS Departamento,
    'Colombia' AS Pais,
    1 AS EsPrincipal
FROM Clientes;

-- Segundas direcciones para el 50%
INSERT INTO DireccionesCliente (ClienteId, TipoDireccion, Direccion, Ciudad, Departamento, Pais, EsPrincipal)
SELECT 
    Id AS ClienteId,
    'TRABAJO' AS TipoDireccion,
    'Carrera ' + CAST(ABS(CHECKSUM(NEWID())) % 150 + 1 AS VARCHAR) + ' # ' + CAST(ABS(CHECKSUM(NEWID())) % 100 + 1 AS VARCHAR) + '-' + CAST(ABS(CHECKSUM(NEWID())) % 100 + 1 AS VARCHAR) AS Direccion,
    CHOOSE((ABS(CHECKSUM(NEWID())) % 10) + 1, 'Bogotá', 'Medellín', 'Cali', 'Barranquilla', 'Cartagena', 'Bucaramanga', 'Pereira', 'Manizales', 'Ibagué', 'Santa Marta') AS Ciudad,
    'Dpto' AS Departamento,
    'Colombia' AS Pais,
    0 AS EsPrincipal
FROM Clientes
WHERE Id % 2 = 0;

-- ContactosCliente
INSERT INTO ContactosCliente (ClienteId, TipoContacto, Valor, EsPrincipal)
SELECT 
    Id AS ClienteId,
    'CELULAR' AS TipoContacto,
    '3' + CAST(100000000 + ABS(CHECKSUM(NEWID())) % 200000000 AS VARCHAR) AS Valor,
    1 AS EsPrincipal
FROM Clientes;

-- Segundos contactos (Email) para el 25%
INSERT INTO ContactosCliente (ClienteId, TipoContacto, Valor, EsPrincipal)
SELECT 
    Id AS ClienteId,
    'EMAIL' AS TipoContacto,
    LOWER(REPLACE(Nombres, ' ', '.') + '.' + REPLACE(Apellidos, ' ', '.') + CAST(Id AS VARCHAR) + '@ejemplo.com') AS Valor,
    0 AS EsPrincipal
FROM Clientes
WHERE Id % 4 = 0;

DROP TABLE #Nombres;
DROP TABLE #Apellidos;

PRINT 'Clientes poblados exitosamente.';
GO
