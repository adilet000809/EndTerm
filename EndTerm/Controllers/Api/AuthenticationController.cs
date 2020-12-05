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

        public AuthenticationController(
            UserManager<IdentityUser> userManager, 
            IConfiguration configuration)
        {
            this._userManager = userManager;
            _configuration = configuration;
        }

        /// <summary>
        /// Authentication endpoint
        /// </summary>
        /// <param name="authRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public IActionResult Login(AuthRequest authRequest)
        {
            IActionResult response = Unauthorized();
            var user = AuthenticateUser(authRequest);
            if (user != null)
            {
                var token = GenerateJWTToken(authRequest);
                response = Ok(new
                {
                    token = token,
                    email = authRequest.Email
                });
            }

            return response;
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
        
        string GenerateJWTToken(AuthRequest authRequest)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email, authRequest.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }    
    
        private IdentityUser AuthenticateUser(AuthRequest authRequest)
        {
            var user = _userManager.FindByEmailAsync(authRequest.Email).Result;
            if (user == null) return null;
            if (!_userManager.CheckPasswordAsync(user, authRequest.Password).Result) return null;
            return user;
        }    
        
    }
}