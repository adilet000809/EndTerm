using System.Collections.Generic;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using EndTerm.Models;
using EndTerm.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EndTerm.Controllers.Api
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public class AdvertisementController : Controller
    {
        
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IOblastRepository _oblastRepository;
        private readonly ICityRepository _cityRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public AdvertisementController(IAdvertisementRepository advertisementRepository, ICategoryRepository categoryRepository, IOblastRepository oblastRepository, ICityRepository cityRepository, UserManager<IdentityUser> userManager)
        {
            _advertisementRepository = advertisementRepository;
            _categoryRepository = categoryRepository;
            _oblastRepository = oblastRepository;
            _cityRepository = cityRepository;
            _userManager = userManager;
        }
        
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        
        [HttpGet("advertisements")]
        public IEnumerable<Advertisement> GetAllAdvertisements()
        {
            return _advertisementRepository.GetAllAdvertisement();
        }
        
        [HttpGet("advertisements/{advertisementId}")]
        public Advertisement GetAdvertisement(int advertisementId)
        {
            return _advertisementRepository.GetAdvertisement(advertisementId);
        }
        
        [HttpPost("advertisements/add")]
        public IActionResult AddAdvertisement(JsonElement request)
        {
            var advertisementName = request.GetProperty("advertisementName").GetString();
            var advertisementImage = request.GetProperty("advertisementImage").GetString();
            var advertisementDescription = request.GetProperty("advertisementDescription").GetString();
            var categoryId = request.GetProperty("categoryId").GetInt32();
            var category = _categoryRepository.GetCategory(categoryId);
            var oblastId = request.GetProperty("oblastId").GetInt32();
            var oblast = _oblastRepository.GetOblast(oblastId);
            var cityId = request.GetProperty("cityId").GetInt32();
            var city = _cityRepository.GetCity(cityId);
            var advertisement = new Advertisement
            {
                Name = advertisementName,
                Image = advertisementImage,
                Description = advertisementDescription,
                Category = category,
                CategoryId = categoryId,
                Oblast = oblast,
                OblastId = oblast.Id,
                City = city,
                CityId = city.Id
            };
            _advertisementRepository.Add(advertisement);
            return Ok("Created successfully");
        }
    }
}