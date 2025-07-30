using System.ComponentModel.DataAnnotations;

namespace APIBiblioteca.DTO.Reportes
{
    public class RespuestaReporteDTO
    {
        [Display(Name = "Usuario")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Display(Name = "Cantidad de prestamos")]
        public int CantidadPrestamos { get; set; }

        public List<string> Libros { get; set; } = new List<string>();
    }
}
