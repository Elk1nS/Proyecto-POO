using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ComputersAPI.Database.Entities
{
    [Table("categories_components")]
    public class CategoryComponentEntity
    {
        [Column("id")]
        [Key]
        public Guid Id { get; set; }

        [Column("type")]
        [Required]
        public string Type { get; set; }

        public virtual ICollection<ComponentEntity> Components { get; set; }

    }
}
