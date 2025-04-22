using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ComputersAPI.Dtos.Components;
using ComputersAPI.Dtos.Peripherals;

namespace ComputersAPI.Dtos.Computers
{
    public class ComputerDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }


        public List<ComponentDto> Components { get; set; }
        public List<PeripheralDto> Peripherals { get; set; }

    }
}   
