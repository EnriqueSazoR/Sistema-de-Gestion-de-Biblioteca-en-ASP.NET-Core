using APIBiblioteca.Data.Repository.IRepository;
using APIBiblioteca.DTO.Prestamos;
using APIBiblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace APIBiblioteca.Data.Repository
{
    public class PrestamoRepository : IPrestamoRepository
    {
        private readonly ApplicationDBContext _db;

        public PrestamoRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<List<RespuestaPrestamoDTO>> GetPrestamos()
        {
            var prestamos = await _db.Prestamos
                .Include(p => p.Usuario)
                .Include(p => p.Libro)
                .Select(p => new RespuestaPrestamoDTO
                {
                    Usuario = p.Usuario.NombreUsuario,
                    Libro = p.Libro.Titulo,
                    FechaPrestamo = p.FechaPrestamo,
                    FechaDevolucion = p.FechaDevolucion
                }).ToListAsync();

            return prestamos;
        }

        public async Task<Prestamo> PostPrestamo(InsertarPrestamoDTO prestamoDTO, int userID)
        {
            var buscarLibro = await _db.Libros.FirstOrDefaultAsync(l => l.Titulo.ToLower() == prestamoDTO.TituloLibro.ToLower());
            if(buscarLibro == null)
            {
                throw new Exception("El libro no existe");
            }

            if(!buscarLibro.Estado)
            {
                throw new Exception("El libro se encuentra en prestamo");
            }

            // ingresar un nuevo prestamo
            var prestamo = new Prestamo
            {
                IdUsuario = userID,
                IdLibro = buscarLibro.Id,
                FechaPrestamo = DateOnly.FromDateTime(DateTime.Now),
                FechaDevolucion = prestamoDTO.FechaDevolucion,
                Estado = true
            };

            // actualizar el estado en la tabla libros
            buscarLibro.Estado = false;
            _db.Libros.Update(buscarLibro);

            // Guardamos el prestamo
            await _db.AddAsync(prestamo);
            await _db.SaveChangesAsync();

            return prestamo;
        }

        public async Task<Libro> UpdatePrestamo(ActualizarPrestamoDTO prestamoDTO, int id)
        {
            var libroDevuelto = await _db.Libros.FindAsync(id);

            if(libroDevuelto == null)
            {
                throw new Exception("Libro no encontrado");
            }

            // Convertir estado
            bool estado;
            switch (prestamoDTO.Estado.Trim().ToLower())
            {
                case "disponible":
                    estado = true;
                    break;

                case "prestado":
                    estado = false;
                    break;

                default:
                    throw new Exception("Estado no permitido");
            }

            libroDevuelto.Estado = estado;
            await _db.SaveChangesAsync();

            return libroDevuelto;

        }
    }
}
