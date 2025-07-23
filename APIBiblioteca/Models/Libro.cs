using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIBiblioteca.Models
{
    public class Libro
    {
        // Propiedades
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El titulo del libro es obligatorio")]
        public string Titulo { get; set; } = string.Empty;
        [Required(ErrorMessage = "El autor es obligatorio")]
        public string Autor { get; set; } = string.Empty;
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string ISBN { get; set; } = string.Empty;
        [Required(ErrorMessage = "El estado es obligatorio")]
        public bool Estado { get; set; }

        // Crear llave foranea
        public int IdCategoria { get; set; }
        [ForeignKey("IdCategoria")]
        public Categoria categoria { get; set; } = null!;

        
            
    }
}
