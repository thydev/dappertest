using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;

namespace dappertest.Models
{

    public class Dojo : BaseEntity
    {
        public Dojo(){
            this.Ninjas = new List<Ninja>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }

        [Required]
        public string Location { get; set; }

        public string Description { get; set; }

        public ICollection<Ninja> Ninjas { get; set; }
    }

}