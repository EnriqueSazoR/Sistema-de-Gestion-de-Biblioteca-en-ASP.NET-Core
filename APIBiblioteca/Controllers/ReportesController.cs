using APIBiblioteca.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace APIBiblioteca.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly IReporteRepository _reporteRepository;

        public ReportesController(IReporteRepository reporteRepository)
        {
            _reporteRepository = reporteRepository;
        }

        // Reporte número 1, cantidad de prestamos por usuarios
        [HttpGet("PrestamosUsuarios")]
        public async Task<ActionResult> PrestamosPorUsuario()
        {
            var reporte = await _reporteRepository.PrestamoPorUsuario();

            return Ok(new { Reporte = reporte });
        }

        // Reporte número 2, cantidad de prestamos por usuario en especial
        [HttpGet("PrestamosUsuario/nombre/{nombreUsuario}")]
        public async Task<ActionResult> PrestamosPorUsuarioNombre(String nombreUsuario)
        {
            try
            {
                var reporte = await _reporteRepository.PrestamoPorUsarioNombre(nombreUsuario);

                return Ok(new { usuario = reporte });
            }catch(Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        // Reporte número 3, libros más prestados
        [HttpGet("LibrosMasPrestados")]
        public async Task<ActionResult> librosMasPrestados()
        {
            var reporte = await _reporteRepository.ObtenerLibrosMasPrestados();

            return Ok(new { libros = reporte });
        }

        // Reporte número 4, validar el estado de un libro
        [HttpGet("EstadoLibro/{titulo}")]
        public async Task<ActionResult> EstadoLibro(string titulo)
        {
            try
            {
                var reporte = await _reporteRepository.EstadoLibroPorNombre(titulo);

                return Ok(new { resultado = reporte });

            }catch(Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
    }
}
