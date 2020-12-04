using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EndTerm.Models
{
    public class City
    {
        [Required]
        public int Id { get; set; }
        [Display(Name = "City")]
        [Required]
        public string Name { get; set; }
        [Required]
        public int OblastId { get; set; }
        [Required]
        [JsonIgnore]
        public Oblast Oblast { get; set; }
    }
}