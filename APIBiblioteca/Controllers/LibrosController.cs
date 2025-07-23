using APIBiblioteca.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using APIBiblioteca.DTO.Libros;
using APIBiblioteca.Models;
using Microsoft.AspNetCore.Authorization;

namespace APIBiblioteca.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly ILibroRepository _libroRepository;

        public LibrosController(ILibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
        }

        // EndPoints
        // Crear libro
        [HttpPost("Crear")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> InsertarLibro([FromBody] InsertarLibroDTO libroDTO)
        {
            // validar modelo
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var libroCreado = await _libroRepository.PostLibro(libroDTO);
                return Ok(new { mensaje = "El libro se creo exitosamente" });
            }catch(Exception e)
            {
                return BadRequest(new {mensaje = e.Message});
            }
        }

        // Obtener todos los libros
        [HttpGet("lista-libros")]
        [Authorize(Roles = "Admin, Bibliotecario")]
        public async Task<ActionResult> ObtenerLibros()
        {
            var libros = await _libroRepository.GetLibroLista();

            return Ok(new {libros =  libros});
        }

        // Obtener un libro por ID
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Bibliotecario")]
        public async Task<ActionResult> ObtenerLibroId(int id)
        {
            try
            {
                var libro = await _libroRepository.GetLibroId(id);
                return Ok(new {libro = libro});
            }catch(Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        // Actualizar Libro
        [Authorize(Roles = "Admin")]
        [HttpPut("Actualizar/{id}")]
        public async Task<ActionResult> ActualizarCategoria(int id, InsertarLibroDTO libroDTO)
        {
            try
            {
                var libroActualizado = await _libroRepository.UpdateLibro(libroDTO,  id);
                return Ok(new { Actualizacion = libroActualizado });
            }catch(Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        // Eliminar Libro
        [Authorize(Roles = "Admin")]
        [HttpDelete("Eliminar/{id}")]
        public async Task<ActionResult> EliminarLibro(int id)
        {
            try
            {
                var libroEliminado = await _libroRepository.DeleteLibro(id);
                return Ok(new { mensaje = "Libro Eliminado" });
            }catch(Exception e)
            {
                return BadRequest(new {error = e.Message});
            }
        }
    }
}
