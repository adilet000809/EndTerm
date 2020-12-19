using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text.Json;
using System.Threading.Tasks;
using EndTerm.Models;
using EndTerm.Models.Request;
using EndTerm.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(AuthenticationSchemes = 
            JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("favourites")]
        public IEnumerable<FavouritesItem> GetFavouritesItems()
        {
            var email = User.Claims.First().Value;
            var favourites = _favouritesRepository.GetAllFavourites().First(
                f => f.UserId == GetCurrentUser(email).Id);
            var favouriteItems = _favouritesItemRepository.GetAllFavouritesItems().Where(
                f => f.FavouritesId == favourites.Id);
            return favouriteItems;

        }
        
        /// <summary>
        /// Add advertisement to favourites
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = 
            JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("favourites/add")]
        public IActionResult AddFavourite(FavouriteItemRequest request)
        {
            var email = User.Claims.First().Value;
            var advertisementId = request.AdvertisementId;
            var advertisement = _advertisementRepository.GetAdvertisement(advertisementId);
            var favourites = _favouritesRepository.GetAllFavourites().LastOrDefault(
                f => f.UserId == GetCurrentUser(email).Id) ?? new Favourites
            {
                User = GetCurrentUser(email),
                UserId = GetCurrentUser(email).Id
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
        
        [Authorize(AuthenticationSchemes = 
            JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("favourites/{favouritesItemId}")]
        public IActionResult DeleteCategory(int favouritesItemId)
        {
            var email = User.Claims.First().Value;
            var user = _userManager.FindByEmailAsync(email).Result;
            var favourites = _favouritesRepository.GetAllFavourites().Last(
                f => f.UserId == GetCurrentUser(email).Id);
            var item = _favouritesItemRepository.GetFavouritesItem(favouritesItemId);
            if (item.FavouritesId != favourites.Id) return BadRequest("This item does not belong to you");
            var result = _favouritesItemRepository.Delete(favouritesItemId);
            if (result == null) return BadRequest("Item not found");  
            return Ok("Deleted from favourites");

        }

        private IdentityUser GetCurrentUser(string email)
        {
            return _userManager.FindByEmailAsync(email).Result;
        }
    }
}