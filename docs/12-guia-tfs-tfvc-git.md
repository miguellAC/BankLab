# Guía de Control de Versiones (TFS, TFVC y Git)

## 1. TFS (Team Foundation Server) y TFVC
Team Foundation Server (ahora Azure DevOps Server) es el ALM (Application Lifecycle Management) tradicional de Microsoft.
TFVC (Team Foundation Version Control) es su sistema de control de versiones centralizado.

### Conceptos Clave TFVC
- **Workspace (Espacio de trabajo):** Mapeo local de las carpetas del servidor. Puede ser Local (soporta trabajo offline) o Server (depende del servidor constante).
- **Check-out:** Marcar un archivo para edición.
- **Check-in:** Enviar los cambios al servidor (similar a Commit + Push).
- **Shelving (Poner en un estante):** Guardar cambios temporalmente en el servidor sin integrarlos a la rama principal (útil para suspender tareas).
- **Branching y Merging:** Modelos basados en carpetas. Es más pesado que en Git.

### Flujo de Trabajo Simulado (tf.exe)
```powershell
# Obtener última versión
tf get $/BankLab/Main /recursive

# Modificar archivo
tf checkout $/BankLab/Main/Docs/archivo.md

# Guardar cambios
tf checkin /comment:"Actualizado documento"
```

## 2. Git
Git es el estándar actual en la industria, distribuido y ligero.

### Conceptos Clave Git
- **Commit:** Guarda cambios locales.
- **Branch:** Referencia ligera a un commit. Crear y cambiar de ramas es barato.
- **Merge/Rebase:** Estrategias para integrar cambios.
- **Pull Request:** Proceso formal de revisión de código antes de un merge en un servidor central (ej. GitHub, Azure Repos).

### Comandos Comunes
```bash
git init
git clone https://...
git add .
git commit -m "feat: agregar validaciones BLL"
git push origin main
git pull
```

## 3. Comparativa: TFVC vs Git

| Característica | TFVC | Git |
|----------------|------|-----|
| Modelo | Centralizado | Distribuido |
| Historial Offline | No (en Server Workspace) | Sí, historial completo local |
| Ramificación | Carpetas físicas, pesadas | Punteros, muy ligeras |
| Bloqueo (Locks) | Sí, explícito | No, resolución de conflictos (Merge) |
| Integración Azure DevOps | Excelente | Excelente |

## 4. Azure Boards y Pipelines

### Work Items (Boards)
- **Epic -> Feature -> User Story -> Task / Bug**
- Los commits o check-ins deben vincularse a un Work Item (ej. `#123`).

### Pipelines (YAML)
```yaml
trigger:
- main
pool:
  vmImage: 'windows-latest'
steps:
- task: DotNetCoreCLI@2
  inputs:
    command: 'build'
    projects: '**/*.csproj'
```
