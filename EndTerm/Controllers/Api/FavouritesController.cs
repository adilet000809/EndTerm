using System.Collections.Generic;
using System.Linq;
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
    public class FavouritesController : Controller
    {
        private readonly IFavouritesRepository _favouritesRepository;
        private readonly IAdvertisementRepository _advertisementRepository;
        private readonly IFavouritesItemRepository _favouritesItemRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public FavouritesController(
            IFavouritesRepository favouritesRepository, 
            UserManager<IdentityUser> userManager, 
            IFavouritesItemRepository favouritesItemRepository, 
            IAdvertisementRepository advertisementRepository)
        {
            _favouritesRepository = favouritesRepository;
            _userManager = userManager;
            _favouritesItemRepository = favouritesItemRepository;
            _advertisementRepository = advertisementRepository;
        }
        
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        
        [HttpGet("favourites")]
        public IEnumerable<FavouritesItem> GetFavouritesItems()
        {
            var favourites = _favouritesRepository.GetAllFavourites().First(
                f => f.UserId == GetCurrentUserAsync().Result.Id);
            var favouriteItems = _favouritesItemRepository.GetAllFavouritesItems().Where(
                f => f.FavouritesId == favourites.Id);
            return favouriteItems;

        }
        
        [HttpPost("favourites/add")]
        public IActionResult AddFavourite(JsonElement request)
        {
            var advertisementId = request.GetProperty("advertisementId").GetInt32();
            var advertisement = _advertisementRepository.GetAdvertisement(advertisementId);
            var favourites = _favouritesRepository.GetAllFavourites().LastOrDefault(
                f => f.UserId == GetCurrentUserAsync().Result.Id) ?? new Favourites
            {
                User = GetCurrentUserAsync().Result,
                UserId = GetCurrentUserAsync().Result.Id
            };

            var favouriteItem = new FavouritesItem
            {
                Advertisement = advertisement,
                AdvertisementId = advertisementId,
                Favourites = favourites,
                FavouritesId = favourites.Id
            };
            _favouritesRepository.AddOrUpdate(favourites);
            _favouritesItemRepository.Add(favouriteItem);
            return Ok("Successful");
        }
        
        [HttpDelete("favourites/{favouritesItemId}")]
        public IActionResult DeleteCategory(int favouritesItemId)
        {
            var result = _favouritesItemRepository.Delete(favouritesItemId);
            if (result == null) return BadRequest("Item not found");  
            return Ok("Deleted from favourites");
        }
    }
}