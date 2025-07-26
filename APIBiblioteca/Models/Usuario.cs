using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [StringLength(255, MinimumLength = 80)]
        public string PasswordHash { get; set; } = string.Empty;

        [JsonIgnore]
        public ICollection<Rol> Roles { get; set; } = new List<Rol>();

        // Propiedad de navegación para saber cuantos usuarios tienen préstamos
        public ICollection<Prestamo> Prestamos { get; set; } = new List<Prestamo>();
    }
}
