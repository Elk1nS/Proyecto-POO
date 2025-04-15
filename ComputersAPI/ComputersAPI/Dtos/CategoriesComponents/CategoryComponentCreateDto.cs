using System.ComponentModel.DataAnnotations;

namespace ComputersAPI.Dtos.CategoriesComponents
{
    public class CategoryComponentCreateDto
    {
        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(15, MinimumLength = 3, ErrorMessage = "El campo {0} debe tener un minimo de {2} y un maximo de {1} caracteres")]
        public string Type { get; set; }
    }
}
