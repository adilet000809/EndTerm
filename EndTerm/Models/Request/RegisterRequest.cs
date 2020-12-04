using System.ComponentModel.DataAnnotations;

namespace EndTerm.Models.Request
{
    public class RegisterRequest
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}