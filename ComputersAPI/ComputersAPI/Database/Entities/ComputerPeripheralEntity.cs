using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputersAPI.Database.Entities
{
    [Table("computers_peripherals")]
    public class ComputerPeripheralEntity
    {
        [Column("id")]
        [Key]
        public Guid Id { get; set; }

        [Column("computer_id")]
        public Guid ComputerId { get; set; }

        [ForeignKey(nameof(ComputerId))]
        public virtual ComputerEntity Computer { get; set; }

        [Column("peripheral_id")]
        public Guid PeripheralId { get; set; }

        [ForeignKey(nameof(PeripheralId))]
        public virtual PeripheralEntity Peripheral { get; set; }
    }
}
