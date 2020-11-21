using System.Collections.Generic;
using System.Net.Mime;
using System.Text.Json;
using EndTerm.Models;
using EndTerm.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EndTerm.Controllers.Api
{
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/[controller]")]
    public class CityController : Controller
    {
        private readonly IOblastRepository _oblastRepository;
        private readonly ICityRepository _cityRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public CityController(
            ICityRepository cityRepository, 
            UserManager<IdentityUser> userManager, 
            IOblastRepository oblastRepository)
        {
            _cityRepository = cityRepository;
            _userManager = userManager;
            _oblastRepository = oblastRepository;
        }
        
        [HttpGet("cities")]
        public IEnumerable<City> GetAllCities()
        {
            return _cityRepository.GetAllCities();
        }
        
        [HttpGet("cities/{cityId}")]
        public City GetCity(int cityId)
        {
            return _cityRepository.GetCity(cityId);
        }
        
        [HttpPost("cities/add")]
        public IActionResult AddCity(JsonElement request)
        {
            var oblastId = request.GetProperty("oblastId").GetInt32();
            var cityName = request.GetProperty("cityName").GetString();
            var oblast = _oblastRepository.GetOblast(oblastId);
            if (oblast == null) return BadRequest("Oblast Not Found");
            _cityRepository.Add(new City { Name = cityName, OblastId = oblastId, Oblast = oblast});
            return Ok("Successful");
        }
        
        [HttpPost("cities/update")]
        public IActionResult UpdateCity(JsonElement request)
        {
            var cityId = request.GetProperty("cityId").GetInt32();
            var cityName = request.GetProperty("cityName").GetString();
            var city = _cityRepository.GetCity(cityId);
            if (city == null) return BadRequest("City Not Found");
            city.Name = cityName;
            _cityRepository.Update(city);
            return Ok("Updated successfully");
        }
        
        [HttpDelete("cities/{cityId}")]
        public IActionResult DeleteCity(int cityId)
        {
            var result = _cityRepository.Delete(cityId);
            if (result == null) return BadRequest("Item not found");  
            return Ok("Deleted successfully");
        }
        
    }
}