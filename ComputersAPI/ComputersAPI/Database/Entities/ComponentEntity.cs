using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputersAPI.Database.Entities
{
    [Table("components")]
    public class ComponentEntity
    {
        [Column("id")]
        [Key]
        public Guid Id { get; set; }

        [Column("brand")]
        [Required]
        public string Brand { get; set; }

        [Column("model")]
        [Required]
        public string Model { get; set; }

        [Column("Price")]
        [Required]
        public decimal Price { get; set; }

        // Relación con CategoryComponent
        [Column("category_component_id")]
        public Guid CategoryComponentId { get; set; }

        [ForeignKey(nameof(CategoryComponentId))]
        public virtual CategoryComponentEntity CategoryComponent { get; set; }
    }
}
