using System.ComponentModel.DataAnnotations;

namespace dappertest.Models
{
    public class Ninja : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        [Range(0, 10)]
        public int Level { get; set; }

        public string Description { get; set; }

        public Dojo Dojo { get; set; }

        public int dojo_id { get; set; }
    }
}