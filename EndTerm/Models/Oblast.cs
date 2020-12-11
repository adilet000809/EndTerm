using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EndTerm.Models
{
    public class Oblast
    {
        
        [Required]
        public int Id { get; set; }
        [Display(Name = "Oblast")]
        [Required]
        public string Name { get; set; }
        public ICollection<City> Cities { get; set; }

        //public List<Advertisement> Advertisements { get; set; }
    }
}