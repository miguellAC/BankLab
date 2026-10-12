USE [BankLabDB];
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarClientePorIdentificacion
-- Descripción: Consulta datos básicos de un cliente por su identificación.
-- ==========================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_ConsultarClientePorIdentificacion]
    @TipoIdentificacionId INT = NULL,
    @NumeroIdentificacion VARCHAR(20),
    @TipoIdentificacion INT = NULL -- Soporte para alias de parámetro
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @TipoId INT = COALESCE(@TipoIdentificacionId, @TipoIdentificacion);

    SELECT 
        c.ClienteId,
        c.TipoIdentificacionId,
        ti.Codigo AS CodigoTipoIdentificacion,
        ti.Nombre AS NombreTipoIdentificacion,
        c.NumeroIdentificacion,
        c.PrimerNombre,
        c.SegundoNombre,
        c.PrimerApellido,
        c.SegundoApellido,
        (c.PrimerNombre + ' ' + ISNULL(c.SegundoNombre + ' ', '') + c.PrimerApellido + ISNULL(' ' + c.SegundoApellido, '')) AS Nombres,
        c.PrimerApellido AS Apellidos,
        c.TipoIdentificacionId AS TipoIdentificacion,
        c.FechaNacimiento,
        c.Genero,
        c.Email,
        c.Telefono,
        c.SucursalId,
        s.Nombre AS NombreSucursal,
        c.Estado,
        c.FechaRegistro
    FROM Clientes c
    INNER JOIN TiposIdentificacion ti ON c.TipoIdentificacionId = ti.TipoIdentificacionId
    LEFT JOIN Sucursales s ON c.SucursalId = s.SucursalId
    WHERE (@TipoId IS NULL OR c.TipoIdentificacionId = @TipoId)
      AND c.NumeroIdentificacion = @NumeroIdentificacion;
END;
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarClientePorId
-- Descripción: Consulta el detalle completo de un cliente por su ID.
-- ==========================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_ConsultarClientePorId]
    @ClienteId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- 1. Cabecera del Cliente
    SELECT 
        c.ClienteId,
        c.TipoIdentificacionId,
        ti.Codigo AS CodigoTipoIdentificacion,
        ti.Nombre AS NombreTipoIdentificacion,
        c.NumeroIdentificacion,
        c.PrimerNombre,
        c.SegundoNombre,
        c.PrimerApellido,
        c.SegundoApellido,
        (c.PrimerNombre + ' ' + ISNULL(c.SegundoNombre + ' ', '') + c.PrimerApellido + ISNULL(' ' + c.SegundoApellido, '')) AS Nombres,
        c.PrimerApellido AS Apellidos,
        c.TipoIdentificacionId AS TipoIdentificacion,
        c.FechaNacimiento,
        c.Genero,
        c.Email,
        c.Telefono,
        c.SucursalId,
        s.Nombre AS NombreSucursal,
        c.Estado,
        c.FechaRegistro
    FROM Clientes c
    INNER JOIN TiposIdentificacion ti ON c.TipoIdentificacionId = ti.TipoIdentificacionId
    LEFT JOIN Sucursales s ON c.SucursalId = s.SucursalId
    WHERE c.ClienteId = @ClienteId;

    -- 2. Direcciones
    SELECT 
        DireccionId, ClienteId, TipoDireccion, Direccion, Ciudad, Departamento, CodigoPostal, Pais, EsPrincipal, Activa, FechaCreacion
    FROM DireccionesCliente 
    WHERE ClienteId = @ClienteId 
    ORDER BY EsPrincipal DESC, FechaCreacion DESC;

    -- 3. Contactos
    SELECT 
        ContactoId, ClienteId, TipoContacto, Valor, EsPrincipal, Activo, FechaCreacion
    FROM ContactosCliente 
    WHERE ClienteId = @ClienteId 
    ORDER BY EsPrincipal DESC, FechaCreacion DESC;
END;
GO

-- ==========================================================
-- Procedimiento: sp_BuscarClientes
-- Descripción: Búsqueda paginada de clientes.
-- ==========================================================
CREATE OR ALTER PROCEDURE [dbo].[sp_BuscarClientes]
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
    FROM Clientes c
    WHERE (@Nombres IS NULL OR (c.PrimerNombre + ' ' + ISNULL(c.SegundoNombre,'')) LIKE '%' + @Nombres + '%')
      AND (@Apellidos IS NULL OR (c.PrimerApellido + ' ' + ISNULL(c.SegundoApellido,'')) LIKE '%' + @Apellidos + '%')
      AND (@NumeroIdentificacion IS NULL OR c.NumeroIdentificacion = @NumeroIdentificacion)
      AND (@Estado IS NULL OR c.Estado = @Estado)
      AND (@SucursalId IS NULL OR c.SucursalId = @SucursalId);

    SELECT 
        c.ClienteId,
        c.TipoIdentificacionId,
        ti.Codigo AS CodigoTipoIdentificacion,
        ti.Nombre AS NombreTipoIdentificacion,
        c.NumeroIdentificacion,
        c.PrimerNombre,
        c.SegundoNombre,
        c.PrimerApellido,
        c.SegundoApellido,
        (c.PrimerNombre + ' ' + ISNULL(c.SegundoNombre + ' ', '') + c.PrimerApellido + ISNULL(' ' + c.SegundoApellido, '')) AS Nombres,
        c.PrimerApellido AS Apellidos,
        c.TipoIdentificacionId AS TipoIdentificacion,
        c.FechaNacimiento,
        c.Genero,
        c.Email,
        c.Telefono,
        c.SucursalId,
        s.Nombre AS NombreSucursal,
        c.Estado,
        c.FechaRegistro
    FROM Clientes c
    INNER JOIN TiposIdentificacion ti ON c.TipoIdentificacionId = ti.TipoIdentificacionId
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
CREATE OR ALTER PROCEDURE [dbo].[sp_RegistrarCliente]
    @TipoIdentificacionId INT = NULL,
    @NumeroIdentificacion VARCHAR(20),
    @PrimerNombre VARCHAR(50) = NULL,
    @SegundoNombre VARCHAR(50) = NULL,
    @PrimerApellido VARCHAR(50) = NULL,
    @SegundoApellido VARCHAR(50) = NULL,
    @Nombres VARCHAR(100) = NULL, -- Alias
    @Apellidos VARCHAR(100) = NULL, -- Alias
    @TipoIdentificacion INT = NULL, -- Alias
    @FechaNacimiento DATE = NULL,
    @Genero CHAR(1) = 'O',
    @Email VARCHAR(100) = NULL,
    @Telefono VARCHAR(20) = NULL,
    @SucursalId INT = 1,
    @Direccion VARCHAR(200) = NULL,
    @Ciudad VARCHAR(100) = 'Bogotá',
    @Usuario VARCHAR(50) = NULL,
    @UsuarioAuditoria VARCHAR(50) = NULL, -- Alias
    @ClienteId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @TId INT = COALESCE(@TipoIdentificacionId, @TipoIdentificacion, 1);
    DECLARE @PNom VARCHAR(50) = COALESCE(@PrimerNombre, PARSENAME(REPLACE(@Nombres, ' ', '.'), 2), @Nombres, 'Nombre');
    DECLARE @PApe VARCHAR(50) = COALESCE(@PrimerApellido, PARSENAME(REPLACE(@Apellidos, ' ', '.'), 2), @Apellidos, 'Apellido');
    DECLARE @Usr VARCHAR(50) = COALESCE(@Usuario, @UsuarioAuditoria, SYSTEM_USER);

    IF EXISTS (SELECT 1 FROM Clientes WHERE TipoIdentificacionId = @TId AND NumeroIdentificacion = @NumeroIdentificacion)
    BEGIN
        RAISERROR('El cliente con esta identificación ya existe.', 16, 1);
        RETURN;
    END

    BEGIN TRY
        BEGIN TRANSACTION;
        
        INSERT INTO Clientes (
            TipoIdentificacionId, NumeroIdentificacion, PrimerNombre, SegundoNombre, PrimerApellido, SegundoApellido,
            FechaNacimiento, Genero, Email, Telefono, SucursalId, Estado, FechaRegistro, UsuarioCreacion
        )
        VALUES (
            @TId, @NumeroIdentificacion, @PNom, @SegundoNombre, @PApe, @SegundoApellido,
            @FechaNacimiento, @Genero, @Email, @Telefono, @SucursalId, 'ACTIVO', GETDATE(), @Usr
        );
        
        SET @ClienteId = SCOPE_IDENTITY();
        
        IF @Direccion IS NOT NULL
        BEGIN
            INSERT INTO DireccionesCliente (ClienteId, TipoDireccion, Direccion, Ciudad, EsPrincipal, Activa)
            VALUES (@ClienteId, 'RESIDENCIA', @Direccion, @Ciudad, 1, 1);
        END
        
        IF @Telefono IS NOT NULL OR @Email IS NOT NULL
        BEGIN
            IF @Telefono IS NOT NULL
                INSERT INTO ContactosCliente (ClienteId, TipoContacto, Valor, EsPrincipal, Activo)
                VALUES (@ClienteId, 'TELEFONO', @Telefono, 1, 1);

            IF @Email IS NOT NULL
                INSERT INTO ContactosCliente (ClienteId, TipoContacto, Valor, EsPrincipal, Activo)
                VALUES (@ClienteId, 'EMAIL', @Email, 0, 1);
        END
        
        INSERT INTO RegistrosAuditoria (TipoEntidad, EntidadId, Accion, ValorNuevo, UsuarioAuditoria, FechaAuditoria)
        VALUES ('Cliente', @ClienteId, 'CREAR', 'Cliente Registrado', @Usr, GETDATE());
        
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
CREATE OR ALTER PROCEDURE [dbo].[sp_ConsultarProductosCliente]
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
    INNER JOIN ProductosBancarios pb ON c.ProductoId = pb.ProductoId
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
