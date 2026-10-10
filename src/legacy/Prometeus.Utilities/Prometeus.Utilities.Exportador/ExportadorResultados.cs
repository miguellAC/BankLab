using System;
using System.Data;
using System.IO;
using System.Text;

namespace Prometeus.Utilities.Exportador
{
    public class ExportadorResultados
    {
        public static void ExportarCSV(DataTable tabla, string rutaArchivo, char delimitador = ',')
        {
            if (tabla == null) throw new ArgumentNullException(nameof(tabla));

            var sb = new StringBuilder();

            // Header
            for (int i = 0; i < tabla.Columns.Count; i++)
            {
                sb.Append(tabla.Columns[i].ColumnName);
                if (i < tabla.Columns.Count - 1) sb.Append(delimitador);
            }
            sb.AppendLine();

            // Rows
            foreach (DataRow row in tabla.Rows)
            {
                for (int i = 0; i < tabla.Columns.Count; i++)
                {
                    string valor = row[i]?.ToString() ?? "";
                    if (valor.Contains(",") || valor.Contains(";") || valor.Contains("\"") || valor.Contains("\n"))
                    {
                        valor = $"\"{valor.Replace("\"", "\"\"")}\"";
                    }
                    sb.Append(valor);
                    if (i < tabla.Columns.Count - 1) sb.Append(delimitador);
                }
                sb.AppendLine();
            }

            File.WriteAllText(rutaArchivo, sb.ToString(), Encoding.UTF8);
        }

        public static void ExportarHTML(DataTable tabla, string rutaArchivo, string titulo = "Reporte Banco Ejemplo")
        {
            if (tabla == null) throw new ArgumentNullException(nameof(tabla));

            var sb = new StringBuilder();
            sb.AppendLine("<!DOCTYPE html>");
            sb.AppendLine("<html lang='es'>");
            sb.AppendLine("<head>");
            sb.AppendLine("  <meta charset='utf-8'>");
            sb.AppendLine($"  <title>{titulo}</title>");
            sb.AppendLine("  <style>");
            sb.AppendLine("    body { font-family: Segoe UI, sans-serif; margin: 20px; background-color: #f8f9fa; }");
            sb.AppendLine("    h2 { color: #003366; }");
            sb.AppendLine("    table { border-collapse: collapse; width: 100%; background: #ffffff; box-shadow: 0 1px 3px rgba(0,0,0,0.1); }");
            sb.AppendLine("    th, td { border: 1px solid #dee2e6; padding: 10px; text-align: left; font-size: 13px; }");
            sb.AppendLine("    th { background-color: #003366; color: white; }");
            sb.AppendLine("    tr:nth-child(even) { background-color: #f2f5f8; }");
            sb.AppendLine("  </style>");
            sb.AppendLine("</head>");
            sb.AppendLine("<body>");
            sb.AppendLine($"  <h2>{titulo}</h2>");
            sb.AppendLine($"  <p>Generado: {DateTime.Now:yyyy-MM-dd HH:mm:ss}</p>");
            sb.AppendLine("  <table>");
            sb.AppendLine("    <thead><tr>");

            foreach (DataColumn col in tabla.Columns)
            {
                sb.AppendLine($"      <th>{col.ColumnName}</th>");
            }
            sb.AppendLine("    </tr></thead>");
            sb.AppendLine("    <tbody>");

            foreach (DataRow row in tabla.Rows)
            {
                sb.AppendLine("    <tr>");
                foreach (DataColumn col in tabla.Columns)
                {
                    sb.AppendLine($"      <td>{row[col]}</td>");
                }
                sb.AppendLine("    </tr>");
            }

            sb.AppendLine("    </tbody>");
            sb.AppendLine("  </table>");
            sb.AppendLine("</body>");
            sb.AppendLine("</html>");

            File.WriteAllText(rutaArchivo, sb.ToString(), Encoding.UTF8);
        }

        public static void ExportarTextoPlano(DataTable tabla, string rutaArchivo)
        {
            ExportarCSV(tabla, rutaArchivo, '\t');
        }
    }
}
