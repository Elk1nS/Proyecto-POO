using ComputersAPI.Dtos.Components;
using ComputersAPI.Dtos.Peripherals;

namespace ComputersAPI.Dtos.Computers
{
    public class ComputerActionResponseDto
    {
        public Guid Id  { get; set; }
        public string Type { get; set; }
        public string Brand { get; set; }

        public List<ComponentDto> Components { get; set; }
        public List<PeripheralDto> Peripherals { get; set; }
    }
}
