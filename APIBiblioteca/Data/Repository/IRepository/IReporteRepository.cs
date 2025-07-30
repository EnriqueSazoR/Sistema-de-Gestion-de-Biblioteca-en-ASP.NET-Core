using APIBiblioteca.DTO.Reportes;
using APIBiblioteca.Models;

namespace APIBiblioteca.Data.Repository.IRepository
{
    public interface IReporteRepository
    {
        // Definir métodos
        Task<List<RespuestaReporteDTO>> PrestamoPorUsuario();
        Task<RespuestaReporteDTO> PrestamoPorUsarioNombre(string nombreUsuario);
        Task<List<LibrosMasPrestadosDTO>> ObtenerLibrosMasPrestados();
        Task<EstadoLibrosDTO> EstadoLibroPorNombre(string titulo);
    }
}
