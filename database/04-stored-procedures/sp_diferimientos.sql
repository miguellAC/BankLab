USE [BankLabDB];
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarClientesIncluidosDiferimiento
-- Descripción: Consulta los clientes incluidos en un diferimiento.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_ConsultarClientesIncluidosDiferimiento]
    @SolicitudDifId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Assuming SolicitudDifId references a hypothetical Diferimientos table setup
    -- Based on prompt, creating generic logic returning clients and operations.
    SELECT 
        c.ClienteId,
        c.NumeroIdentificacion,
        c.PrimerNombre + ' ' + c.PrimerApellido AS NombreCompleto,
        oc.NumeroOperacion,
        oc.OperacionId
    FROM Clientes c
    INNER JOIN OperacionesCredito oc ON c.ClienteId = oc.ClienteId
    -- In a real scenario, this joins with a ClientesIncluidosDiferimiento table
    -- For BankLab mockup, we will just return a placeholder or mock structure based on the request.
    -- (No exact schema was given for diferimientos, so this represents the expected JOIN)
    -- WHERE oc.OperacionId IN (SELECT OperacionId FROM OperacionesDiferidas WHERE SolicitudDifId = @SolicitudDifId)
    WHERE 1=0; -- Placeholder pending actual schema details.
END;
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarClientesExcluidosDiferimiento
-- Descripción: Consulta los clientes excluidos en un diferimiento.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_ConsultarClientesExcluidosDiferimiento]
    @SolicitudDifId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.ClienteId,
        c.NumeroIdentificacion,
        c.PrimerNombre + ' ' + c.PrimerApellido AS NombreCompleto
    FROM Clientes c
    WHERE 1=0; -- Placeholder
END;
GO

-- ==========================================================
-- Procedimiento: sp_ConsultarOperacionesDiferidas
-- Descripción: Consulta las operaciones diferidas.
-- ==========================================================
CREATE PROCEDURE [dbo].[sp_ConsultarOperacionesDiferidas]
    @SolicitudDifId INT = NULL,
    @ClienteId INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        oc.OperacionId,
        oc.NumeroOperacion,
        c.PrimerNombre + ' ' + c.PrimerApellido AS Cliente
    FROM OperacionesCredito oc
    INNER JOIN Clientes c ON oc.ClienteId = c.ClienteId
    WHERE (@ClienteId IS NULL OR oc.ClienteId = @ClienteId)
      AND 1=0; -- Placeholder
END;
GO
