using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputersAPI.Database.Entities
{
    [Table("computers")]
    public class ComputerEntity
    {
        [Column("id")]
        [Required]
        public Guid Id { get; set; }

        [Column("type")]
        [Required]
        public string Type { get; set; }

        [Column("brand")]
        public string Brand { get; set; }

        [Column("total_price")]
        public decimal TotalPrice { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }


        public virtual ICollection<ComputerComponentEntity> ComputerComponents { get; set; }
        public virtual ICollection<ComputerPeripheralEntity> ComputerPeripherals { get; set; }

    }
}
