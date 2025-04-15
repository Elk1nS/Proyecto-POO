using ComputersAPI.Dtos.Components;
using System.ComponentModel.DataAnnotations;

namespace ComputersAPI.Dtos.Peripherals
{
    public class PeripheralCreateDto
    {
        [Display(Name = "Marca")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(15, MinimumLength = 2, ErrorMessage = "El campo {0} debe tener un minimo de {2} y un maximo de {1} caracteres")]
        public string Brand { get; set; }

        [Display(Name = "Modelo")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "El campo {0} debe tener un minimo de {2} y un maximo de {1} caracteres")]
        public string Model { get; set; }

        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public decimal Price { get; set; }

        [Display(Name = "category_component_id")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public Guid CategoryPeripheralId { get; set; }
    }
}
