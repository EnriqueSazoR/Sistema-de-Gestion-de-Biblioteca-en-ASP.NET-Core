using APIBiblioteca.DTO.Libros;
using APIBiblioteca.Models;

namespace APIBiblioteca.Data.Repository.IRepository
{
    public interface ILibroRepository
    {
        // Definir métodos
        Task<Libro> PostLibro(InsertarLibroDTO libroDTO);
        Task<List<RespuestaLibroDTO>> GetLibroLista();
        Task<RespuestaLibroDTO> GetLibroId(int id);
        Task<Libro> UpdateLibro(InsertarLibroDTO libroDTO, int id);
        Task<Libro> DeleteLibro(int id);

    }
}
