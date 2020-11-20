using System.ComponentModel.DataAnnotations;

namespace EndTerm.Models
{
    public class Advertisement
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "Product")]
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public Category Category { get; set; }
        [Required]
        public int AddressId { get; set; }
        [Required]
        public Address Address { get; set; }
    }
}
