using System.ComponentModel.DataAnnotations;

namespace EndTerm.Models.Request
{
    public class OblastRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}