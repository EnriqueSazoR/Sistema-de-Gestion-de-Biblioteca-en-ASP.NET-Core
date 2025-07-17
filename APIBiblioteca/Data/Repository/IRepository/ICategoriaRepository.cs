using APIBiblioteca.Models;

namespace APIBiblioteca.Data.Repository.IRepository
{
    public interface ICategoriaRepository
    {
        // Definir métodos, serán asincronos en el controlador
        Task<Categoria> PostCategoria(Categoria categoria);
        Task<List<Categoria>> GetCategorias();
        Task<Categoria> PutCategoria(int id, Categoria categoria);
    }
}
