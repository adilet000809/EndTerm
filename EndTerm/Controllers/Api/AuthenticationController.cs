using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using EndTerm.Jwt;
using EndTerm.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EndTerm.Controllers.Api
{
    [Produces("application/json")]
    [ApiController] 
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;  
        private readonly IConfiguration _configuration;
        private UserService _userService;

        public AuthenticationController(
            UserManager<IdentityUser> userManager, 
            IConfiguration configuration, UserService userService)
        {
            this._userManager = userManager;
            _configuration = configuration;
            _userService = userService;
        }
        
        /// <summary>
        /// Authentication endpoint
        /// </summary>
        /// <param name="authRequest"></param>
        /// <returns></returns>
        [HttpPost]  
        [Route("login")]  
        public async Task<IActionResult> Login(AuthRequest authRequest)
        {
            var response = _userService.Authenticate(authRequest).Result;
            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });
            return Ok(response);
        }  
        
        /// <summary>
        /// Registration endpoint
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
        [HttpPost]  
        [Route("register")]  
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            var emailExists = await _userManager.FindByEmailAsync(registerRequest.Email);  
            var userNameExists = await _userManager.FindByNameAsync(registerRequest.Username);  
            if (emailExists != null)  
                return StatusCode(StatusCodes.Status400BadRequest, "Email already registered!");  
            if (userNameExists != null)  
                return StatusCode(StatusCodes.Status400BadRequest, "Username is not available!");
  
            var user = new IdentityUser()  
            {  
                Email = registerRequest.Email,  
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = registerRequest.Username,
                
            };  
            var result = await _userManager.CreateAsync(user, registerRequest.Password);  
            return !result.Succeeded ? StatusCode(StatusCodes.Status500InternalServerError, "User creation failed! Please check user details and try again.") : Ok("User created successfully!");
        }
        
    }
}