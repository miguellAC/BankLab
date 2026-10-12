-- 02-clientes.sql
-- Generates 2,000 clients, with addresses and contacts
-- Approx: 2000 Clientes, 3000 DireccionesCliente, 2500 ContactosCliente

SET NOCOUNT ON;
GO

USE BankLabDB;
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
INSERT INTO Clientes (ClienteId, SucursalId, TipoIdentificacionId, NumeroIdentificacion, PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido, FechaNacimiento, Genero, Email, Telefono, Estado, FechaRegistro)
SELECT 
    N AS ClienteId,
    ((N - 1) % 10) + 1 AS SucursalId,
    CASE 
        WHEN N % 100 < 80 THEN 1 -- CC (80%)
        WHEN N % 100 < 90 THEN 3 -- CE (10%)
        WHEN N % 100 < 95 THEN 2 -- NIT (5%)
        WHEN N % 100 < 98 THEN 4 -- TI (3%)
        ELSE 5 -- PA (2%)
    END AS TipoIdentificacionId,
    CAST(1000000000 + N AS VARCHAR) AS NumeroIdentificacion,
    (SELECT TOP 1 Nombre FROM #Nombres WHERE Id = ((N * 7) % 50) + 1) AS PrimerNombre,
    CASE WHEN N % 2 = 0 THEN (SELECT TOP 1 Nombre FROM #Nombres WHERE Id = ((N * 13) % 50) + 1) ELSE NULL END AS SegundoNombre,
    (SELECT TOP 1 Apellido FROM #Apellidos WHERE Id = ((N * 11) % 40) + 1) AS PrimerApellido,
    (SELECT TOP 1 Apellido FROM #Apellidos WHERE Id = ((N * 17) % 40) + 1) AS SegundoApellido,
    DATEADD(DAY, -((N * 13) % 20000) - 6570, GETDATE()) AS FechaNacimiento, -- 18 to 72 years ago
    CASE WHEN N % 2 = 0 THEN 'M' ELSE 'F' END AS Genero,
    'cliente' + CAST(N AS VARCHAR) + '@ejemplo.com' AS Email,
    '300' + RIGHT('0000000' + CAST(N AS VARCHAR), 7) AS Telefono,
    CASE 
        WHEN N % 100 < 90 THEN 'ACTIVO'
        WHEN N % 100 < 95 THEN 'INACTIVO'
        WHEN N % 100 < 98 THEN 'BLOQUEADO'
        ELSE 'FALLECIDO'
    END AS Estado,
    DATEADD(DAY, -((N * 5) % 3000), GETDATE()) AS FechaRegistro
FROM Nums;
SET IDENTITY_INSERT Clientes OFF;

-- DireccionesCliente
-- Check constraint: TipoDireccion IN ('RESIDENCIA','TRABAJO','CORRESPONDENCIA','OTRA')
INSERT INTO DireccionesCliente (ClienteId, TipoDireccion, Direccion, Ciudad, Departamento, Pais, EsPrincipal)
SELECT 
    ClienteId,
    'RESIDENCIA' AS TipoDireccion,
    'Calle ' + CAST((ClienteId % 150) + 1 AS VARCHAR) + ' # ' + CAST((ClienteId % 100) + 1 AS VARCHAR) + '-' + CAST(((ClienteId * 3) % 100) + 1 AS VARCHAR) AS Direccion,
    CHOOSE((ClienteId % 10) + 1, 'Bogotá', 'Medellín', 'Cali', 'Barranquilla', 'Cartagena', 'Bucaramanga', 'Pereira', 'Manizales', 'Ibagué', 'Santa Marta') AS Ciudad,
    'Dpto' AS Departamento,
    'Colombia' AS Pais,
    1 AS EsPrincipal
FROM Clientes;

-- Segundas direcciones para el 50%
INSERT INTO DireccionesCliente (ClienteId, TipoDireccion, Direccion, Ciudad, Departamento, Pais, EsPrincipal)
SELECT 
    ClienteId,
    'TRABAJO' AS TipoDireccion,
    'Carrera ' + CAST((ClienteId % 150) + 1 AS VARCHAR) + ' # ' + CAST((ClienteId % 100) + 1 AS VARCHAR) + '-' + CAST(((ClienteId * 3) % 100) + 1 AS VARCHAR) AS Direccion,
    CHOOSE((ClienteId % 10) + 1, 'Bogotá', 'Medellín', 'Cali', 'Barranquilla', 'Cartagena', 'Bucaramanga', 'Pereira', 'Manizales', 'Ibagué', 'Santa Marta') AS Ciudad,
    'Dpto' AS Departamento,
    'Colombia' AS Pais,
    0 AS EsPrincipal
FROM Clientes
WHERE ClienteId % 2 = 0;

-- ContactosCliente
-- Check constraint: TipoContacto IN ('TELEFONO','CELULAR','EMAIL','FAX')
INSERT INTO ContactosCliente (ClienteId, TipoContacto, Valor, EsPrincipal)
SELECT 
    ClienteId,
    'CELULAR' AS TipoContacto,
    '3' + RIGHT('000000000' + CAST(100000000 + ClienteId AS VARCHAR), 9) AS Valor,
    1 AS EsPrincipal
FROM Clientes;

-- Segundos contactos (Email) para el 25%
INSERT INTO ContactosCliente (ClienteId, TipoContacto, Valor, EsPrincipal)
SELECT 
    ClienteId,
    'EMAIL' AS TipoContacto,
    LOWER(PrimerNombre + '.' + PrimerApellido + CAST(ClienteId AS VARCHAR) + '@ejemplo.com') AS Valor,
    0 AS EsPrincipal
FROM Clientes
WHERE ClienteId % 4 = 0;

DROP TABLE #Nombres;
DROP TABLE #Apellidos;

PRINT 'Clientes poblados exitosamente.';
GO
