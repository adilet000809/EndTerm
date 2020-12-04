using System.ComponentModel.DataAnnotations;

namespace EndTerm.Models.Request
{
    public class AdvertisementRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int OblastId { get; set; }
        [Required]
        public int CityId { get; set; }
    }
}