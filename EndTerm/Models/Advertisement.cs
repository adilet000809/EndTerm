using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace EndTerm.Models
{
    public class Advertisement: IValidatableObject
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "Advertisement")]
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
        public string UserId { get; set; }
        [Required]
        public IdentityUser User { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var errors = new List<ValidationResult>();
            
            if (string.IsNullOrWhiteSpace(this.Name))
                errors.Add(new ValidationResult("Enter name"));
            
            if (this.Description.Length<25)
                errors.Add(new ValidationResult("Description is to short"));

            return errors;
        }
        
    }
}
