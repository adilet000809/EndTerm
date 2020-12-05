using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using EndTerm.Models;
using EndTerm.Models.Request;
using EndTerm.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EndTerm.Controllers.Api
{
    [Produces("application/json")]
    [ApiController]
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
        
        /// <summary>
        /// Fetch all advertisements
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = 
        JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("advertisements")]
        public IEnumerable<Advertisement> GetAllAdvertisements()
        {
            var a = User.Claims.First().Value;
            if (User.Identity != null)
            {
                var b = User.Identity.Name;
            }
            return _advertisementRepository.GetAllAdvertisement();
        }
        
        /// <summary>
        /// Fetch single advertisement by id
        /// </summary>
        /// <param name="advertisementId"></param>
        /// <returns></returns>
        [HttpGet("advertisements/{advertisementId}")]
        public IActionResult GetAdvertisement(int advertisementId)
        {
            var result = _advertisementRepository.GetAdvertisement(advertisementId);
            if (result == null) return NotFound("Advertisement not found");
            return Ok(result);
        }
        
        /// <summary>
        /// Add advertisement into database
        /// </summary>
        /// <param name="advertisementRequest"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = 
            JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("advertisements/add")]
        public IActionResult AddAdvertisement(AdvertisementRequest advertisementRequest)
        {
            var userEmail = User.Claims.First().Value;
            var user = _userManager.FindByEmailAsync(userEmail).Result;
            var category = _categoryRepository.GetCategory(advertisementRequest.CategoryId);
            if (category == null) return NotFound("Category not found");
            var oblast = _oblastRepository.GetOblast(advertisementRequest.OblastId);
            if (oblast == null) return NotFound("Oblast not found");
            var city = _cityRepository.GetCity(advertisementRequest.CityId);
            if (city == null) return NotFound("City not found");
            var advertisement = new Advertisement
            {
                Name = advertisementRequest.Name,
                Image = advertisementRequest.Image,
                Description = advertisementRequest.Description,
                Category = category,
                CategoryId = category.Id,
                Oblast = oblast,
                OblastId = oblast.Id,
                City = city,
                CityId = city.Id,
                User = user,
                UserId = user.Id
            };
            _advertisementRepository.Add(advertisement);
            return Ok("Created successfully");
        }
    }
}