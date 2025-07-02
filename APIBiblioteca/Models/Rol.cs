using System.ComponentModel.DataAnnotations;

namespace APIBiblioteca.Models
{
    public class Rol
    {
        // Propiedades
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El Rol es obligatorio")]
        public string NombreRol { get; set; } = string.Empty;

        // Navegación a tabla intermedia
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
