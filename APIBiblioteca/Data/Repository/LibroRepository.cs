using APIBiblioteca.Data.Repository.IRepository;
using APIBiblioteca.DTO.Libros;
using APIBiblioteca.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace APIBiblioteca.Data.Repository
{

    public class LibroRepository : ILibroRepository
    {
        private readonly ApplicationDBContext _db;

        public LibroRepository(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<RespuestaLibroDTO> GetLibroId(int id)
        {
            var libroBuscado = await _db.Libros
                .Include(l => l.categoria)
                .FirstOrDefaultAsync(l => l.Id == id);
                
            if (libroBuscado == null)
            {
                throw new Exception("Libro Inexistente");
            }

            var respuestaLibro = new RespuestaLibroDTO
            {
                Titulo = libroBuscado.Titulo,
                Autor = libroBuscado.Autor,
                ISBN = libroBuscado.ISBN,
                Categoria = libroBuscado.categoria.NombreCategoria,
                Estado = libroBuscado.Estado ? "disponible" : "prestado"
            };

            return respuestaLibro;
        }

        public async Task<List<RespuestaLibroDTO>> GetLibroLista()
        {
            var listaLibros = await _db.Libros
                .Include(l => l.categoria)
                .ToListAsync();

            var libros = listaLibros.Select(l => new RespuestaLibroDTO
            {
                Titulo = l.Titulo,
                Autor = l.Autor,
                ISBN = l.ISBN,
                Categoria = l.categoria.NombreCategoria,
                Estado = l.Estado ? "disponible" : "prestado"
            }).ToList();

            return libros;
        }

        public async Task<Libro> PostLibro(InsertarLibroDTO libroDTO)
        {
            // validar si existe el libro
            var libroExistente = await _db.Libros.AnyAsync(l => l.Titulo.ToLower() == libroDTO.Titulo.ToLower());
            if (libroExistente)
            {
                throw new Exception("Error, el libro ya existe en la bd");
            }

            // Convertir estado
            bool estado;
            switch (libroDTO.Estado.Trim().ToLower())
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

            // Buscar categoría por nombre
            var categoria = await _db.Categorias.FirstOrDefaultAsync(c => c.NombreCategoria.ToLower() == libroDTO.Categoria.ToLower());
            if (categoria == null)
            {
                throw new Exception("La categoría ingresada no existe");
            }

            // Crear libro
            var libroNuevo = new Libro
            {
                Titulo = libroDTO.Titulo,
                Autor = libroDTO.Autor,
                ISBN = libroDTO.ISBN,
                IdCategoria = categoria.Id,
                Estado = estado
            };

            // Guardar cambios
            await _db.AddAsync(libroNuevo);
            await _db.SaveChangesAsync();

            return libroNuevo;
        }

        public async Task<Libro> UpdateLibro(InsertarLibroDTO libroDTO, int id)
        {
            var buscarLibro = await _db.Libros.FindAsync(id);
            if (buscarLibro == null)
            {
                throw new Exception("Libro Inexistente");
            }

            var categoria = await _db.Categorias.FirstOrDefaultAsync(c => c.NombreCategoria.ToLower() == libroDTO.Categoria.ToLower());
            if (categoria == null)
            {
                throw new Exception("La categoría ingresada no existe");
            }

            // Cambiar los datos
            buscarLibro.Titulo = libroDTO.Titulo;
            buscarLibro.Autor = libroDTO.Autor;
            buscarLibro.ISBN = libroDTO.ISBN;
            buscarLibro.IdCategoria = categoria.Id;

            // Guardar Cambios
            var libroActualizado = buscarLibro;
            await _db.SaveChangesAsync();
            return libroActualizado;
        }

        public async Task<Libro> DeleteLibro(int id)
        {
            var libroEliminado = await _db.Libros.FindAsync(id);
            if(libroEliminado == null)
            {
                throw new Exception("No se encontró el libro");
            }
            // Eliminar Libro
            _db.Libros.Remove(libroEliminado);
            await _db.SaveChangesAsync();

            return libroEliminado;

        }
    }
}
