using System.ComponentModel.DataAnnotations;

namespace APIBiblioteca.DTO.Autenticacion
{
    public class UsuarioLoginDTO
    {
        [Required]
        [EmailAddress(ErrorMessage = "El campo email no cumple con el formato válido")]
        public string Correo { get; set; }

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        public string Password { get; set; }
    }
}
