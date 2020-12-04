using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EndTerm.Models
{
    public class FavouritesItem
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int AdvertisementId { get; set; }
        [Required]
        public Advertisement Advertisement { get; set; }
        [Required]
        public int FavouritesId { get; set; }
        [Required]
        public Favourites Favourites { get; set; }
    }
}