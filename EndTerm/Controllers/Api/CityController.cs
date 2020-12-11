using System.Collections.Generic;
using System.Net.Mime;
using System.Text.Json;
using EndTerm.Models;
using EndTerm.Models.Request;
using EndTerm.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EndTerm.Controllers.Api
{
    [Produces("application/json")]
    [ApiController]
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
        
        /// <summary>
        /// Fetch all cities
        /// </summary>
        /// <returns></returns>
        [HttpGet("cities")]
        public IEnumerable<City> GetAllCities()
        {
            return _cityRepository.GetAllCities();
        }
        
        /// <summary>
        /// Fetch single city by id
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        [HttpGet("cities/{cityId}")]
        public IActionResult GetCity(int cityId)
        {
            var result = _cityRepository.GetCity(cityId);
            if (result == null) return NotFound("City not found");
            return Ok(result);
        }
        
        /// <summary>
        /// Add city into database
        /// </summary>
        /// <param name="cityRequest"></param>
        /// <returns></returns>
        [HttpPost("cities/add")]
        public IActionResult AddCity(CityRequest cityRequest)
        {
            var oblastId = cityRequest.OblastId;
            var cityName = cityRequest.Name;
            var oblast = _oblastRepository.GetOblast(oblastId);
            if (oblast == null) return NotFound("Oblast Not Found");
            var city = new City
            {
                Name = cityName,
                OblastId = oblastId,
                Oblast = oblast
            };
            oblast.Cities.Add(city);
            _cityRepository.Add(city);
            _oblastRepository.Update(oblast);
            return Ok("Successful");
        }
        
        /// <summary>
        /// Update city
        /// </summary>
        /// <param name="cityRequest"></param>
        /// <returns></returns>
        [HttpPut("cities/update")]
        public IActionResult UpdateCity(CityRequest cityRequest)
        {
            var city = _cityRepository.GetCity(cityRequest.Id);
            var oblast = _oblastRepository.GetOblast(cityRequest.OblastId);
            if (city == null) return NotFound("City Not Found");
            if (oblast == null) return NotFound("Oblast Not Found");
            city.Name = city.Name;
            city.OblastId = oblast.Id;
            city.Oblast = oblast;
            _cityRepository.Update(city);
            return Ok("Updated successfully");
        }
        
        /// <summary>
        /// Delete city
        /// </summary>
        /// <param name="cityId"></param>
        /// <returns></returns>
        [HttpDelete("cities/{cityId}")]
        public IActionResult DeleteCity(int cityId)
        {
            var result = _cityRepository.Delete(cityId);
            if (result == null) return NotFound("City not found");  
            return Ok("Deleted successfully");
        }
        
    }
}