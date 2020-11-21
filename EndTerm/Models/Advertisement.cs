using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

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
        public int OblastId { get; set; }
        [Required]
        public Oblast Oblast { get; set; }
        [Required]
        public int CityId { get; set; }
        [Required]
        public City City { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public IdentityUser User { get; set; }
    }
}
