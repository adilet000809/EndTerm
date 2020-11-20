using System.ComponentModel.DataAnnotations;

namespace EndTerm.Models
{
    public class Address
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "Address")]
        [Required]
        public string Oblast { get; set; }
        [Required]
        public string City { get; set; }
    }
}