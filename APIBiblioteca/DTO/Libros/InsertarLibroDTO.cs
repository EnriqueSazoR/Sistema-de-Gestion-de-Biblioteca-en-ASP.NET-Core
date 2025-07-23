using System.ComponentModel.DataAnnotations;

namespace APIBiblioteca.DTO.Libros
{
    public class InsertarLibroDTO
    {
        // Propiedades
        [Required(ErrorMessage = "El Título es obligatorio")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El Autor del libro es obligatorio")]
        public string Autor { get; set; } = string.Empty;

        [Required(ErrorMessage = "ISBN es obligatorio")]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "El ISBN debe contener exactamente 13 digitos")]
        [RegularExpression(@"^\d{13}$", ErrorMessage = "El ISBN debe contener solo números")]
        public string ISBN { get; set; } = string.Empty;

        [Required(ErrorMessage = "Estado es obligatorio - Disponible o Préstado")]
        public string Estado { get; set; } = string.Empty;

        [Required(ErrorMessage = "La categoría es obligatoria")]
        public string Categoria { get; set; } = string.Empty;
    }
}
