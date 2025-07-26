using System.ComponentModel.DataAnnotations;

namespace APIBiblioteca.DTO.Prestamos
{
    public class InsertarPrestamoDTO
    {
        // Propiedades
        [Required(ErrorMessage = "El nombre del libro es obligatorio")]
        [Display(Name = "Libro")]
        public string TituloLibro { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha es obligatoria")]
        [Display(Name = "Fecha de devolución")]
        public DateOnly FechaDevolucion { get; set; }
    }
}
