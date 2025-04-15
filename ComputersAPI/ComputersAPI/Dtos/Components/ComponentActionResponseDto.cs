using ComputersAPI.Dtos.CategoriesComponents;

namespace ComputersAPI.Dtos.Components
{
    public class ComponentActionResponseDto
    {
        public Guid Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryComponentId { get; set; }
        public CategoryComponentDto Category { get; set; }
    }
}
