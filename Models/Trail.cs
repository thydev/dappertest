using System.ComponentModel.DataAnnotations;

namespace dappertest.Models
{
    public class Trail : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        public string Description { get; set; }

        public double Length { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}