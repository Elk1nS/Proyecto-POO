using System.ComponentModel.DataAnnotations;

namespace ComputersAPI.Dtos.Computers
{
    public class ComputerCreateDto
    {
        [Display(Name = "Tipo")]
        [Required(ErrorMessage = "El campo tipo es requerido")]
        [StringLength(15, MinimumLength = 6, ErrorMessage = "El campo {0} debe tener un minimo de {2} y un maximo de {1} caracteres")]
        public string Type { get; set; }

        [Display(Name ="Marca")]
        public string Brand { get; set; }

        //Lista de Componentes y Perifericos
        public List<Guid> ComponentsIds { get; set; }
        public List<Guid> PeripheralsIds { get; set; }
    }
}
