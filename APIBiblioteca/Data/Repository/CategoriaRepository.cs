using APIBiblioteca.Data.Repository.IRepository;
using APIBiblioteca.DTO.Categorias;
using APIBiblioteca.Models;
using Microsoft.EntityFrameworkCore;

namespace APIBiblioteca.Data.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDBContext _db;
        public CategoriaRepository(ApplicationDBContext db)
        {
            _db = db;
        }
        public async Task<List<Categoria>> GetCategorias()
        {
            var categorias = await _db.Categorias.ToListAsync();
            return categorias;
        }

        public async Task<Categoria> PostCategoria(Categoria categoria)
        {
            // validar si la categoría existe
            var categoriaExistente = await _db.Categorias.AnyAsync(c => c.NombreCategoria == categoria.NombreCategoria);
            if (categoriaExistente)
            {
                throw new Exception("La categoría ingresada ya existe en la base de datos..");
            }

            await _db.AddAsync(categoria);
            await _db.SaveChangesAsync();
            return categoria;
        }

    
        public async Task<Categoria> PutCategoria(int id, Categoria categoria)
        {
            var categoriaEncontrada = await _db.Categorias.FirstOrDefaultAsync(c => c.Id == id);

            if (categoriaEncontrada == null)
            {
                throw new Exception("No se encontró la categoria");
            }

            // Actualizar campos
            categoriaEncontrada.NombreCategoria = categoria.NombreCategoria;

            // Actualizar y guardar
            await _db.SaveChangesAsync();
            return categoriaEncontrada;
        }
    }
}
