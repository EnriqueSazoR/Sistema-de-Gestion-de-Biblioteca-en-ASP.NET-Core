using System.ComponentModel.DataAnnotations;

namespace APIBiblioteca.Models
{
    public class Categoria
    {
        // Propiedades
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "La categoría es obligatoria")]
        public string NombreCategoria { get; set; } = string.Empty;

        // Propiedad de navegación
        public List<Libro> Libro { get; set; } = null!;
    }
}
