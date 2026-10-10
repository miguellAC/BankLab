using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Ejemplo.ShellBP
{
    public class ModuleLoader
    {
        private readonly Dictionary<string, Type> _formularios = new Dictionary<string, Type>();

        public void CargarModulos(string directorio = null)
        {
            string dir = directorio ?? AppDomain.CurrentDomain.BaseDirectory;
            if (!Directory.Exists(dir)) return;

            var archivos = Directory.GetFiles(dir, "Ejemplo.GUI.*.dll");

            foreach (var archivo in archivos)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(archivo);
                    var tiposForm = assembly.GetTypes()
                        .Where(t => t.IsSubclassOf(typeof(Form)) && !t.IsAbstract && t.IsPublic);

                    foreach (var tipo in tiposForm)
                    {
                        string key = $"{tipo.Namespace}.{tipo.Name}";
                        _formularios[key] = tipo;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error cargando módulo {archivo}: {ex.Message}");
                }
            }
        }

        public void RegistrarTipo(string key, Type formType)
        {
            _formularios[key] = formType;
        }

        public IEnumerable<KeyValuePair<string, Type>> ObtenerFormulariosDisponibles()
        {
            return _formularios;
        }

        public Form CrearFormulario(string key)
        {
            if (_formularios.TryGetValue(key, out Type tipo))
            {
                try
                {
                    return (Form)Activator.CreateInstance(tipo);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al instanciar formulario {key}: {ex.Message}", "Error de Carga",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return null;
        }
    }
}
