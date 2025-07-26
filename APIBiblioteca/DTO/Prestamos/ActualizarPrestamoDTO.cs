using System.ComponentModel.DataAnnotations;

namespace APIBiblioteca.DTO.Prestamos
{
    public class ActualizarPrestamoDTO
    {
        [Required(ErrorMessage = "El estado es obligatorio")]
        public string Estado { get; set; } = string.Empty;
    }
}
