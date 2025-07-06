Este proyecto demuestra desarrollo backend en un contexto de biblioteca, cubre conceptos clave: autenticación con JWT, autorización por roles, diseño
de bases de datos relacionales con Entity Framework Core y consultas avanzadas para reportes.

# Funcionalidades que tendrá el proyecto
- Registro y Login
- Gestión de Libros
- Gestión de Préstamos
- Reportes

# Roles
- Lectores - Clientes
- Bibliotecarios
- Administradores

# Arquitectura Utilizada
- Capas
  - Controladores
  - Servicios
  - Repositorios
  - Modelos
  - Clases DTO

# Endpoints hasta el momento
- Autenticación
   - POST /api/auth/registro
   - POST /api/auth/asignar-rol
