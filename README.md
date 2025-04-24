# 🛡️ SQL Server Backup Tool

Una herramienta de consola en .NET para generar respaldos completos (estructura + datos) de bases de datos en **SQL Server**, lista para ambientes de desarrollo, auditorías o copias manuales.

---

## 📌 Características

- 🔧 Lee la configuración desde `appsettings.json`.
- 📦 Genera scripts `.sql` con:
  - `CREATE TABLE` incluyendo tipos, longitudes y columnas identidad.
  - `INSERT INTO` con todos los datos de cada tabla (excepto columnas identity).
- 📁 Crea automáticamente la carpeta de respaldo si no existe.
- 🔄 Permite personalizar la conexión y destino del backup.
- 🧱 Arquitectura desacoplada con inyección de dependencias.
- 🧾 Registro de actividad y errores con `ILogger`.

##📂 Estructura del proyecto

├── Application
│   ├── Interfaces
│   └── Services
├── Configuration
│   └── AppConfiguration.cs
├── Shared
│   └── Util
│       └── Print.cs
├── BackupTool
│   └── Program.cs
├── appsettings.json

##✅ Requisitos
-.NET 7.0 SDK
-SQL Server Local o remoto accesible
-Permisos para lectura/escritura de archivos y conexión al servidor

##Autor
-Desarrollado por David Orrego Diaz
-📧 Contacto: [orregod091@gmail.com]
