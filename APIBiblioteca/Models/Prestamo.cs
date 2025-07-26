using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIBiblioteca.Models
{
    public class Prestamo
    {
        // Propiedades
        [Key]
        public int Id { get; set; }

        public int IdUsuario { get; set; }
        [ForeignKey("IdUsuario")]
        public Usuario? Usuario { get; set; }

        public int IdLibro { get; set; }
        [ForeignKey("IdLibro")]
        public Libro? Libro { get; set; }
        
        public DateOnly FechaPrestamo { get; set; }

        [Required(ErrorMessage = "La fecha de devolución es obligatoria")]
        public DateOnly FechaDevolucion { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        public bool Estado { get; set; }

    }
}
