-- 1. All credit operations with client, product, branch, balance, and risk classification
SELECT 
    o.OperacionId,
    c.NombreCompleto,
    p.NombreProducto,
    s.NombreSucursal,
    o.Saldo,
    r.ClasificacionRiesgo
FROM Operaciones o
JOIN Clientes c ON o.ClienteId = c.ClienteId
JOIN Productos p ON o.ProductoId = p.ProductoId
JOIN Sucursales s ON o.SucursalId = s.SucursalId
LEFT JOIN Riesgos r ON o.RiesgoId = r.RiesgoId;

-- 2. Payment history with client details and installment info
SELECT 
    p.PagoId,
    c.NombreCompleto,
    cu.NumeroCuota,
    p.Monto,
    p.FechaPago
FROM Pagos p
JOIN Cuotas cu ON p.CuotaId = cu.CuotaId
JOIN Operaciones o ON cu.OperacionId = o.OperacionId
JOIN Clientes c ON o.ClienteId = c.ClienteId;

-- 3. Overdue portfolio with collection actions and risk history
SELECT 
    o.OperacionId,
    o.Saldo,
    c.NombreCompleto,
    cu.DiasMora,
    r.AccionCobranza
FROM Operaciones o
JOIN Clientes c ON o.ClienteId = c.ClienteId
JOIN Cuotas cu ON o.OperacionId = cu.OperacionId
JOIN Riesgos r ON o.RiesgoId = r.RiesgoId
WHERE cu.DiasMora > 0;

-- 4. Account movements summary by channel and month
SELECT 
    MONTH(m.FechaMovimiento) AS Mes,
    ch.NombreCanal,
    SUM(m.Monto) AS TotalMovimientos
FROM Movimientos m
JOIN Canales ch ON m.CanalId = ch.CanalId
GROUP BY MONTH(m.FechaMovimiento), ch.NombreCanal;

-- 5. Clients with multiple products across different branches
SELECT 
    c.NombreCompleto,
    COUNT(DISTINCT p.ProductoId) AS NumProductos,
    COUNT(DISTINCT s.SucursalId) AS NumSucursales
FROM Clientes c
JOIN Operaciones o ON c.ClienteId = o.ClienteId
JOIN Productos p ON o.ProductoId = p.ProductoId
JOIN Sucursales s ON o.SucursalId = s.SucursalId
GROUP BY c.NombreCompleto
HAVING COUNT(DISTINCT p.ProductoId) > 1 AND COUNT(DISTINCT s.SucursalId) > 1;
