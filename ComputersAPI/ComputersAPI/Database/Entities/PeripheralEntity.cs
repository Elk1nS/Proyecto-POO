using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ComputersAPI.Database.Entities
{
    [Table("peripherals")]
    public class PeripheralEntity
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

        // Relación con CategoryPeripheral
        [Column("category_peripheral_id")]
        public Guid CategoryPeripheralId { get; set; }

        [ForeignKey(nameof(CategoryPeripheralId))]
        public virtual CategoryPeripheralEntity CategoryPeripheral { get; set; }
    }
}
