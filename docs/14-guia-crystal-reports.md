# Guía de Crystal Reports

## ¿Qué es Crystal Reports?
SAP Crystal Reports es una herramienta clásica y poderosa para el diseño y generación de reportes, ampliamente utilizada en aplicaciones .NET Framework.

## Componentes
- **Diseñador (Designer):** Integrado en Visual Studio (requiere instalación separada del "SAP Crystal Reports for Visual Studio").
- **Runtime:** DLLs requeridas en el servidor o cliente final para visualizar/exportar reportes. Existe versión de 32 bits y 64 bits (deben coincidir con el Target CPU de la aplicación).

## Conceptos en la Creación de Reportes
- **Data Source (Origen de datos):** En arquitecturas n-capas como BankLab, NO se conecta directo a SQL en el reporte (para evitar acoplamiento y control de seguridad). Se usan **DataSets (XSD)** tipados cargados dinámicamente con DTOs desde el servicio WCF.
- **Campos, Secciones y Grupos:** Cabecera (encabezado de página), Detalles (líneas), Pie de página. Posibilidad de agrupar por campos.
- **Fórmulas y Parámetros:** Lógica embebida en el `.rpt` (ej. cálculo de impuestos o formato específico).

## Integración en WinForms
Uso del componente `CrystalReportViewer`:
```csharp
// FrmReporte.cs
var listaDto = _agente.ObtenerDatosReporte();
var rpt = new RptClientes();
rpt.SetDataSource(listaDto);
crystalReportViewer1.ReportSource = rpt;
```

## Alternativas Modernas
Debido a las dificultades de licenciamiento, requerimientos de instalación (Runtimes pesados) y falta de soporte en .NET Core / .NET 8, se recomiendan alternativas:
1. **Exportación Simple:** DataGridView + Exportar a CSV / Excel (usando ClosedXML o EPPlus).
2. **Web:** SSRS (SQL Server Reporting Services), PowerBI, o librerías HTML a PDF (PuppeteerSharp, iText).
3. **Migración:** Para nuevas APIs, retornar JSON crudo y delegar la renderización al frontend (React, Angular).
