using System.ComponentModel.DataAnnotations;

namespace APIBiblioteca.DTO.Autenticacion
{
    public class UsuarioRegistroDTO
    {
        [Required(ErrorMessage = "El nombre de ususario es obligatorio")]
        public string NombreUsuario { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "El correo no tiene el formato válido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }
    }
}
