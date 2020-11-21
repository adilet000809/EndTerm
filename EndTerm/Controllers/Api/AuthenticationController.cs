using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EndTerm.Controllers.Api
{
    [ApiController] 
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;  
        private readonly IConfiguration _configuration;

        public AuthenticationController(
            UserManager<IdentityUser> userManager, 
            IConfiguration configuration)
        {
            this._userManager = userManager;
            _configuration = configuration;
        }
        
        [HttpPost]  
        [Route("login")]  
        public async Task<IActionResult> Login(JsonElement request)  
        {  
            var user = await _userManager.FindByEmailAsync(request.GetProperty("email").GetString());  
            if (user != null && await _userManager.CheckPasswordAsync(user, request.GetProperty("password").GetString()))  
            {  
                // var userRoles = await _userManager.GetRolesAsync(user);  
                //
                // var authClaims = new List<Claim>  
                // {  
                //     new Claim(ClaimTypes.Name, user.UserName),  
                //     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
                // };  
                //
                // foreach (var userRole in userRoles)  
                // {  
                //     authClaims.Add(new Claim(ClaimTypes.Role, userRole));  
                // }  
  
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));  
  
                var token = new JwtSecurityToken(  
                    issuer: _configuration["JWT:ValidIssuer"],  
                    audience: _configuration["JWT:ValidAudience"],  
                    expires: DateTime.Now.AddHours(3),  
                    //claims: authClaims,  
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)  
                );  
  
                return Ok(new  
                {  
                    token = new JwtSecurityTokenHandler().WriteToken(token),  
                    expiration = token.ValidTo  
                });  
            }  
            return Unauthorized();  
        }  
        
        [HttpPost]  
        [Route("register")]  
        public async Task<IActionResult> Register(JsonElement request)
        {
            var userExists = await _userManager.FindByEmailAsync(request.GetProperty("email").GetString());  
            if (userExists != null)  
                return StatusCode(StatusCodes.Status500InternalServerError, "User already exists!");  
  
            var user = new IdentityUser()  
            {  
                Email = request.GetProperty("email").GetString(),  
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = request.GetProperty("email").GetString()
            };  
            var result = await _userManager.CreateAsync(user, request.GetProperty("password").GetString());  
            if (!result.Succeeded)  
                return StatusCode(StatusCodes.Status500InternalServerError, "User creation failed! Please check user details and try again.");  
  
            return Ok("User created successfully!");  
        }
        
    }
}