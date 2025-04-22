using ComputersAPI.Dtos.CategoriesPeripherals;
using ComputersAPI.Dtos.Components;

namespace ComputersAPI.Dtos.Peripherals
{
    public class PeripheralActionResponseDto
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryPeripheralId { get; set; }
       // public CategoryPeripheralDto Category { get; set; }
    }
}
