using System.Collections.Generic;
using System.Net.Mime;
using System.Text.Json;
using EndTerm.Models;
using EndTerm.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EndTerm.Controllers.Api
{
    
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class OblastController : Controller
    {
        private readonly IOblastRepository _oblastRepository;
        private readonly UserManager<IdentityUser> _userManager;

        public OblastController(
            IOblastRepository oblastRepository, 
            UserManager<IdentityUser> userManager)
        {
            _oblastRepository = oblastRepository;
            _userManager = userManager;
        }
        
        /// <summary>
        /// Fetch all oblasts
        /// </summary>
        /// <returns>List of oblasts</returns>
        [HttpGet("oblasts")]
        public IEnumerable<Oblast> GetAllOblasts()
        {
            return _oblastRepository.GetAllOblasts();
        }
        
        /// <summary>
        /// Fetch single oblast by id
        /// </summary>
        /// <param name="oblastsId"></param>
        /// <returns></returns>
        [HttpGet("oblasts/{oblastsId}")]
        public IActionResult GetOblast(int oblastsId)
        {
            var result = _oblastRepository.GetOblast(oblastsId);
            if (result == null)
            {
                return NotFound("Oblast not found");
            }

            return Ok(result);
        }
        
        /// <summary>
        /// Add oblast into database
        /// </summary>
        /// <param name="oblast"></param>
        /// <returns></returns>
        [HttpPost("oblasts/add")]
        public Oblast AddOblast(Oblast oblast)
        {
            return _oblastRepository.Add(new Oblast {Name = oblast.Name});
        }
        
        /// <summary>
        /// Update oblast 
        /// </summary>
        /// <param name="oblast"></param>
        /// <returns></returns>
        [HttpPut("oblasts/update")]
        public IActionResult UpdateOblast(Oblast oblast)
        {
            var result = _oblastRepository.GetOblast(oblast.Id);
            if (result == null) return NotFound("Not Found");
            result.Name = oblast.Name;
            _oblastRepository.Update(result);
            return Ok("Updated successfully");
        }
        
        /// <summary>
        /// Delete oblast
        /// </summary>
        /// <param name="oblastsId"></param>
        /// <returns></returns>
        [HttpDelete("oblasts/{oblastsId}")]
        public IActionResult DeleteOblast(int oblastsId)
        {
            var result = _oblastRepository.Delete(oblastsId);
            if (result == null) return NotFound("Item not found");  
            return Ok("Deleted successfully");
        }
        
    }
}