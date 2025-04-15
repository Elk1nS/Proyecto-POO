using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ComputersAPI.Database.Entities
{
    [Table("computers_components")]
    public class ComputerComponentEntity
    {
        [Column("id")]
        [Key]
        public Guid Id { get; set; }

        [Column("computer_id")]
        public Guid ComputerId { get; set; }

        [ForeignKey(nameof(ComputerId))]
        public virtual ComputerEntity Computer { get; set; }

        [Column("component_id")]
        public Guid ComponentId { get; set; }

        [ForeignKey(nameof(ComponentId))]
        public virtual ComponentEntity Component { get; set; }
    }
}
