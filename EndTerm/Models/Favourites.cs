using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EndTerm.Models
{
    public class Favourites
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public IdentityUser User { get; set; }
    }
}