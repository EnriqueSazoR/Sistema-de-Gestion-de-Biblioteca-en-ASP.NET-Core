using APIBiblioteca.Data.Repository.IRepository;
using APIBiblioteca.Models;
using Microsoft.AspNetCore.Mvc;
using APIBiblioteca.DTO.Categorias;
using Microsoft.AspNetCore.Authorization;

namespace APIBiblioteca.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriasController(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        // Definir nuetros Endpoints

        // Endpoint para crear categoría
        [Authorize(Roles = "Admin")]
        [HttpPost("Crear")]
        public async Task<ActionResult> InsertarCategoria([FromBody] InsertarCategoriaDTO categoriaDTO)
        {
            try
            {
                var nuevaCategoria = new Categoria
                {
                    NombreCategoria = categoriaDTO.NombreCategoria
                };

                await _categoriaRepository.PostCategoria(nuevaCategoria);

                return Ok(new
                {
                    mensaje = "Categoría Guardada"
                });

            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }

        }

        // Endpoint para obtener toda las categoría
        [Authorize(Roles = "Admin , Bibliotecario")]
        [HttpGet("lista-categorias")]
        public async Task<ActionResult> GetCategorias()
        {
            var categorias = await _categoriaRepository.GetCategorias();

            return Ok(new
            {
                Categorias = categorias.ToList()
            });
        }

        // Enpoint para actualizar categoria
        [Authorize(Roles = "Admin")]
        [HttpPut("Actualizar/{id}")]
        public async Task<ActionResult> PutCategoria(int id, InsertarCategoriaDTO categoriaDTO)
        {
            try
            {
                   
                var categoriaActualizada = new Categoria
                {
                    NombreCategoria = categoriaDTO.NombreCategoria
                };

                await _categoriaRepository.PutCategoria(id , categoriaActualizada);

                return Ok(new
                {
                    success = "Categoría Actualizada"
                });

            }catch(Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }
    }
}
