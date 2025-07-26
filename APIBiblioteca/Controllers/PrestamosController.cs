using APIBiblioteca.Data.Repository.IRepository;
using APIBiblioteca.DTO.Prestamos;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APIBiblioteca.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PrestamosController : ControllerBase
    {
        private readonly IPrestamoRepository _prestamoRepository;

        public PrestamosController(IPrestamoRepository prestamoRepository)
        {
            _prestamoRepository = prestamoRepository;
        }

        // Endpoints
        // Crear Prestamo
        [HttpPost("Prestamo")]
        public async Task<ActionResult> InsertarPrestamo([FromBody] InsertarPrestamoDTO prestamoDTO)
        {
            try
            {
                var userID = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                var prestamo = await _prestamoRepository.PostPrestamo(prestamoDTO, userID);

                return Ok(new
                {
                    mensaje = "Prestamo realizado correctamente"
                });

            }catch(Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        // Ver prestamos por usuario
        [HttpGet()]
        public async Task<ActionResult> VerPrestamos()
        {
            var listaPrestamos = await _prestamoRepository.GetPrestamos();

            return Ok(new { lista = listaPrestamos });
        }

        // Actualizar estado cuando un libro ha sido devuelto
        [HttpPut("Devolver/{id}")]
        public async Task<ActionResult> ActualizarPrestamo(int id, ActualizarPrestamoDTO prestamoDTO)
        {
            try
            {
                var prestamoActualizado = await _prestamoRepository.UpdatePrestamo(prestamoDTO, id);

                return Ok(new { Actualizacion = "Correcta" });
            }
            catch(Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
    }
}
