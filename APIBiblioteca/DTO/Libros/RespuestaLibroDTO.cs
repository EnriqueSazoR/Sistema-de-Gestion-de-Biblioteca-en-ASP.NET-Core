using APIBiblioteca.Models;
using System.ComponentModel.DataAnnotations;

namespace APIBiblioteca.DTO.Libros
{
    public class RespuestaLibroDTO
    {
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
    }
}
