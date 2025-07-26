using System.ComponentModel.DataAnnotations;

namespace APIBiblioteca.DTO.Prestamos
{
    public class RespuestaPrestamoDTO
    {
        // Propiedades
        public string Usuario { get; set; } = string.Empty;
        public string Libro { get; set; } = string.Empty;

        [Display(Name = "Fecha de préstamo")]
        public DateOnly FechaPrestamo { get; set; }
        [Display(Name = "Fecha de devolución")]
        public DateOnly FechaDevolucion { get; set; }
    }
}
