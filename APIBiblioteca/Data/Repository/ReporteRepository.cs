using APIBiblioteca.Data.Repository.IRepository;
using APIBiblioteca.Models;
using Microsoft.EntityFrameworkCore;
using APIBiblioteca.DTO.Reportes;

namespace APIBiblioteca.Data.Repository
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly ApplicationDBContext _db;

        public ReporteRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<EstadoLibrosDTO> EstadoLibroPorNombre(string titulo)
        {
            var libro = await _db.Libros.FirstOrDefaultAsync(l => l.Titulo.ToLower() == titulo.ToLower());
            if(libro == null)
            {
                throw new Exception("El libro no existe");
            }

            bool estado = await _db.Libros
                .AnyAsync(p => p.Id == libro.Id);

            return new EstadoLibrosDTO
            {
                Titulo = libro.Titulo,
                Estado = estado ? "disponible" : "prestado"
            };
        }

        public async Task<List<LibrosMasPrestadosDTO>> ObtenerLibrosMasPrestados()
        {
            var libros = await _db.Prestamos
                .Include(p => p.Libro)
                .GroupBy(p => new { p.Libro.Id, p.Libro.Titulo })
                .Select(g => new LibrosMasPrestadosDTO
                {
                    Titulo = g.Key.Titulo,
                    CantidadPrestamos = g.Count()
                })
                .OrderByDescending(l => l.CantidadPrestamos)
                .ToListAsync();

            return libros;
        }

        public async Task<RespuestaReporteDTO> PrestamoPorUsarioNombre(string nombreUsuario)
        {
            var usuario = await _db.Usuarios.FirstOrDefaultAsync(u => u.NombreUsuario.ToLower() == nombreUsuario.ToLower());
            if(usuario == null)
            {
                throw new Exception("El usuario no existe");
            }

            var prestamos = await _db.Prestamos
                .Include(p => p.Libro)
                .Where(p => p.Usuario.Id == usuario.Id)
                .ToListAsync();

            if(prestamos.Count == 0)
            {
                throw new Exception("El usuario no tiene prestamos");
            }

            return new RespuestaReporteDTO
            {
                NombreUsuario = usuario.NombreUsuario,
                CantidadPrestamos = prestamos.Count,
                Libros = prestamos.Select(p => p.Libro.Titulo).Distinct().ToList()
            };
        }

        public async Task<List<RespuestaReporteDTO>> PrestamoPorUsuario()
        {
            var prestamoUsuarios = await _db.Prestamos
                 .Include(p => p.Usuario)
                 .Include(p => p.Libro)
                 .GroupBy(p => new { p.Usuario.Id, p.Usuario.NombreUsuario })
                 .Select(g => new RespuestaReporteDTO
                 {
                     NombreUsuario = g.Key.NombreUsuario,
                     CantidadPrestamos = g.Count(),
                     Libros = g.Select(p => p.Libro.Titulo).Distinct().ToList()
                 }).ToListAsync();

            return prestamoUsuarios;
        }
    }
}
