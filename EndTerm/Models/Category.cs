using System.ComponentModel.DataAnnotations;

namespace EndTerm.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "Category")]
        [Required]
        public string Name { get; set; }
    }
}