Este proyecto demuestra desarrollo backend en un contexto de biblioteca, cubre conceptos clave: autenticación con JWT, autorización por roles, diseño
de bases de datos relacionales con Entity Framework Core y consultas avanzadas para reportes.

# Funcionalidades que tendrá el proyecto
- Registro y Login
- Gestión de Categorías
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

# Servicios
- Servicio para generar tokens

# Endpoints
- Autenticación
   - POST /Auth/registro
   - POST /Auth/asignar-rol
   - POST /Auth/login

- Categorías
   - POST /Categorias/Crear
   - GET /Categorias/lista-categorias
   - PUT /Categorias/Actualizar/{id}
 
- Libros
   - POST /Libros/Crear
   - GET /Libros/{id}
   - GET /Libros/lista-libros
   - PUT /Libros/Actualizar/{id}
   - DELETE /Libros/Eliminar/{id}
 
- Prestamos
    - POST /Pretamos/Prestamo
    - GET /Prestamos
    - PUT /Prestamos/Devolver/{id}

- Reporteria
     - GET /Reportes/PrestamosUsuarios
     - GET /Reportes/PrestamosUsuario/nombre/{nombreUsuario}
     - GET /Reportes/LibrosMasPrestados
     - GET /Reportes/EstadoLibro/{titulo}
