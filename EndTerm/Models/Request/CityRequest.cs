using System.ComponentModel.DataAnnotations;

namespace EndTerm.Models.Request
{
    public class CityRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int OblastId { get; set; }
    }
}