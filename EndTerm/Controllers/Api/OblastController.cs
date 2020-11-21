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
        
        [HttpGet("oblasts")]
        public IEnumerable<Oblast> GetAllOblasts()
        {
            return _oblastRepository.GetAllOblasts();
        }
        
        [HttpGet("oblasts/{oblastsId}")]
        public Oblast GetOblast(int oblastsId)
        {
            return _oblastRepository.GetOblast(oblastsId);
        }
        
        [HttpPost("oblasts/add")]
        public Oblast AddOblast(JsonElement request)
        {
            var oblastName = request.GetProperty("oblastName").GetString();
            return _oblastRepository.Add(new Oblast {Name = oblastName});
        }
        
        [HttpPost("oblasts/update")]
        public IActionResult UpdateOblast(JsonElement request)
        {
            var oblastId = request.GetProperty("oblastId").GetInt32();
            var oblastName = request.GetProperty("oblastName").GetString();
            var result = _oblastRepository.GetOblast(oblastId);
            if (result == null) return BadRequest("Not Found");
            result.Name = oblastName;
            _oblastRepository.Update(result);
            return Ok("Updated successfully");
        }
        
        [HttpDelete("oblasts/{oblastsId}")]
        public IActionResult DeleteOblast(int oblastsId)
        {
            var result = _oblastRepository.Delete(oblastsId);
            if (result == null) return BadRequest("Item not found");  
            return Ok("Deleted successfully");
        }
        
    }
}