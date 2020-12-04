using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
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
        [JsonIgnore]
        public IdentityUser User { get; set; }
    }
}