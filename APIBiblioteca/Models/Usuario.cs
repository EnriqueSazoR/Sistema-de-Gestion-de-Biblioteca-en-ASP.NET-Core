using System.ComponentModel.DataAnnotations;

namespace APIBiblioteca.Models
{
    public class Usuario
    {
        // Propiedades
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string NombreUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no tiene un formato válido")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(25, MinimumLength = 12, ErrorMessage = "La contraseña debe tener entre 12 y 25 caracteres")]
        public string PasswordHash { get; set; } = string.Empty;

        // Propiedad de navegación a tabla intermedia
        public ICollection<Rol> Roles { get; set; } = new List<Rol>();
    }
}
