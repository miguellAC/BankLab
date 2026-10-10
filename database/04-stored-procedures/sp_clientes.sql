USE [BankLabDB];
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarClientePorIdentificacion
-- Descripción: Consulta datos básicos de un cliente por su identificación.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_ConsultarClientePorIdentificacion]
    @TipoIdentificacionId INT,
    @NumeroIdentificacion VARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.ClienteId,
        c.PrimerNombre,
        c.SegundoNombre,
        c.PrimerApellido,
        c.SegundoApellido,
        c.FechaNacimiento,
        c.Genero,
        c.Estado,
        ti.Nombre AS TipoIdentificacion,
        c.NumeroIdentificacion,
        s.Nombre AS Sucursal,
        d.Direccion,
        d.Ciudad,
        co.Email,
        co.Telefono
    FROM Clientes c
    INNER JOIN TiposIdentificacion ti ON c.TipoIdentificacionId = ti.TipoIdentificacionId
    LEFT JOIN Sucursales s ON c.SucursalId = s.SucursalId
    LEFT JOIN (
        SELECT ClienteId, Direccion, Ciudad,
               ROW_NUMBER() OVER(PARTITION BY ClienteId ORDER BY FechaRegistro DESC) as rn
        FROM DireccionesCliente
    ) d ON c.ClienteId = d.ClienteId AND d.rn = 1
    LEFT JOIN (
        SELECT ClienteId, Email, Telefono,
               ROW_NUMBER() OVER(PARTITION BY ClienteId ORDER BY FechaRegistro DESC) as rn
        FROM ContactosCliente
    ) co ON c.ClienteId = co.ClienteId AND co.rn = 1
    WHERE c.TipoIdentificacionId = @TipoIdentificacionId
      AND c.NumeroIdentificacion = @NumeroIdentificacion;
END;
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarClientePorId
-- Descripción: Consulta el detalle completo de un cliente por su ID.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_ConsultarClientePorId]
    @ClienteId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.*,
        ti.Nombre AS TipoIdentificacion,
        s.Nombre AS Sucursal
    FROM Clientes c
    INNER JOIN TiposIdentificacion ti ON c.TipoIdentificacionId = ti.TipoIdentificacionId
    LEFT JOIN Sucursales s ON c.SucursalId = s.SucursalId
    WHERE c.ClienteId = @ClienteId;

    SELECT * FROM DireccionesCliente WHERE ClienteId = @ClienteId ORDER BY FechaRegistro DESC;
    SELECT * FROM ContactosCliente WHERE ClienteId = @ClienteId ORDER BY FechaRegistro DESC;
END;
GO

-- ==========================================================
-- Procedimiento: sp_BuscarClientes
-- Descripción: Búsqueda paginada de clientes.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_BuscarClientes]
    @Nombres VARCHAR(100) = NULL,
    @Apellidos VARCHAR(100) = NULL,
    @NumeroIdentificacion VARCHAR(20) = NULL,
    @Estado VARCHAR(20) = NULL,
    @SucursalId INT = NULL,
    @PageNumber INT = 1,
    @PageSize INT = 20
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;

    SELECT COUNT(*) AS TotalRegistros
    FROM Clientes
    WHERE (@Nombres IS NULL OR (PrimerNombre + ' ' + ISNULL(SegundoNombre,'')) LIKE '%' + @Nombres + '%')
      AND (@Apellidos IS NULL OR (PrimerApellido + ' ' + ISNULL(SegundoApellido,'')) LIKE '%' + @Apellidos + '%')
      AND (@NumeroIdentificacion IS NULL OR NumeroIdentificacion = @NumeroIdentificacion)
      AND (@Estado IS NULL OR Estado = @Estado)
      AND (@SucursalId IS NULL OR SucursalId = @SucursalId);

    SELECT 
        c.ClienteId,
        c.PrimerNombre,
        c.PrimerApellido,
        c.NumeroIdentificacion,
        c.Estado,
        s.Nombre AS Sucursal
    FROM Clientes c
    LEFT JOIN Sucursales s ON c.SucursalId = s.SucursalId
    WHERE (@Nombres IS NULL OR (c.PrimerNombre + ' ' + ISNULL(c.SegundoNombre,'')) LIKE '%' + @Nombres + '%')
      AND (@Apellidos IS NULL OR (c.PrimerApellido + ' ' + ISNULL(c.SegundoApellido,'')) LIKE '%' + @Apellidos + '%')
      AND (@NumeroIdentificacion IS NULL OR c.NumeroIdentificacion = @NumeroIdentificacion)
      AND (@Estado IS NULL OR c.Estado = @Estado)
      AND (@SucursalId IS NULL OR c.SucursalId = @SucursalId)
    ORDER BY c.PrimerApellido, c.PrimerNombre
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
END;
GO

-- ==========================================================
-- Procedimiento: sp_RegistrarCliente
-- Descripción: Registra un cliente junto con sus direcciones y contactos.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_RegistrarCliente]
    @TipoIdentificacionId INT,
    @NumeroIdentificacion VARCHAR(20),
    @PrimerNombre VARCHAR(50),
    @SegundoNombre VARCHAR(50) = NULL,
    @PrimerApellido VARCHAR(50),
    @SegundoApellido VARCHAR(50) = NULL,
    @FechaNacimiento DATE,
    @Genero CHAR(1),
    @Email VARCHAR(100),
    @Telefono VARCHAR(20),
    @SucursalId INT,
    @Direccion VARCHAR(200),
    @Ciudad VARCHAR(100),
    @Usuario VARCHAR(50),
    @ClienteId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (SELECT 1 FROM Clientes WHERE TipoIdentificacionId = @TipoIdentificacionId AND NumeroIdentificacion = @NumeroIdentificacion)
    BEGIN
        RAISERROR('El cliente con esta identificación ya existe.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
        
        INSERT INTO Clientes (TipoIdentificacionId, NumeroIdentificacion, PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido, FechaNacimiento, Genero, Estado, SucursalId, FechaRegistro, UsuarioRegistro)
        VALUES (@TipoIdentificacionId, @NumeroIdentificacion, @PrimerNombre, @SegundoNombre, @PrimerApellido, @SegundoApellido, @FechaNacimiento, @Genero, 'ACTIVO', @SucursalId, GETDATE(), @Usuario);
        
        SET @ClienteId = SCOPE_IDENTITY();
        
        INSERT INTO DireccionesCliente (ClienteId, Direccion, Ciudad, FechaRegistro)
        VALUES (@ClienteId, @Direccion, @Ciudad, GETDATE());
        
        INSERT INTO ContactosCliente (ClienteId, Email, Telefono, FechaRegistro)
        VALUES (@ClienteId, @Email, @Telefono, GETDATE());
        
        INSERT INTO RegistrosAuditoria (TipoEntidad, EntidadId, Accion, ValorNuevo, Usuario, FechaRegistro)
        VALUES ('Cliente', @ClienteId, 'CREAR', 'Cliente Registrado', @Usuario, GETDATE());
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
        
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END;
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarProductosCliente
-- Descripción: Consulta cuentas y operaciones de crédito de un cliente.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_ConsultarProductosCliente]
    @ClienteId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        'CUENTA' AS TipoProductoGlobal,
        c.CuentaId AS ProductoId,
        c.NumeroCuenta AS NumeroProducto,
        pb.Nombre AS NombreProducto,
        tc.Nombre AS TipoEspecifico,
        c.Estado,
        c.SaldoDisponible AS SaldoPrincipal,
        c.FechaApertura AS FechaCreacion
    FROM Cuentas c
    INNER JOIN ProductosBancarios pb ON c.ProductoBancarioId = pb.ProductoBancarioId
    INNER JOIN TiposCuenta tc ON c.TipoCuentaId = tc.TipoCuentaId
    WHERE c.ClienteId = @ClienteId

    UNION ALL
    
    SELECT 
        'CREDITO' AS TipoProductoGlobal,
        oc.OperacionId AS ProductoId,
        oc.NumeroOperacion AS NumeroProducto,
        pc.Nombre AS NombreProducto,
        'CREDITO' AS TipoEspecifico,
        oc.Estado,
        so.SaldoTotal AS SaldoPrincipal,
        oc.FechaDesembolso AS FechaCreacion
    FROM OperacionesCredito oc
    INNER JOIN ProductosCredito pc ON oc.ProductoCreditoId = pc.ProductoCreditoId
    INNER JOIN SaldosOperacion so ON oc.OperacionId = so.OperacionId
    WHERE oc.ClienteId = @ClienteId;
END;
GO
