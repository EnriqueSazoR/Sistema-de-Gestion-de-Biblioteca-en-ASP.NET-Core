using APIBiblioteca.DTO.Prestamos;
using APIBiblioteca.Models;

namespace APIBiblioteca.Data.Repository.IRepository
{
    public interface IPrestamoRepository
    {
        // Metodos
        Task<Prestamo> PostPrestamo(InsertarPrestamoDTO prestamoDTO, int userID);
        Task<List<RespuestaPrestamoDTO>> GetPrestamos();
        Task<Libro> UpdatePrestamo(ActualizarPrestamoDTO prestamoDTO, int id);

    }
}
