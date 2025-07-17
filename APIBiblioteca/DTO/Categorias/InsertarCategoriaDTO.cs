using System.ComponentModel.DataAnnotations;

namespace APIBiblioteca.DTO.Categorias
{
    public class InsertarCategoriaDTO
    {
        // propiedades que tendrá la clase
        [Required(ErrorMessage = "El nombre de la categoría es obligatorio")]
        [Display(Name = "Categoría")]
        public string NombreCategoria { get; set; } = string.Empty;
    }
}
