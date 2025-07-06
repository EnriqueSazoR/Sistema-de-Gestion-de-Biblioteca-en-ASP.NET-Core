using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

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
        [JsonIgnore]
        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}
