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
        public Usuario? usuario { get; set; }

        public int IdLibro { get; set; }
        [ForeignKey("IdLibro")]
        public Libro? libro { get; set; }
        
        public DateTime FechaPrestamo { get; set; }

        [Required(ErrorMessage = "La fecha de devolución es obligatoria")]
        public DateTime? FechaDevolución { get; set; }

        [Required(ErrorMessage = "El estado es obligatorio")]
        public bool Estado { get; set; }

    }
}
