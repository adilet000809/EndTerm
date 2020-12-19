using System.ComponentModel.DataAnnotations;

namespace EndTerm.Models.Request
{
    public class FavouriteItemRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public int AdvertisementId { get; set; }
    }
}