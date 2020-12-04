using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EndTerm.Models.Request;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace EndTerm.Jwt
{
    public class UserService
    {
        
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        
        public IEnumerable<IdentityUser> GetAll()
        {
            return _userManager.Users;
        }

        public IdentityUser GetByEmail(string email)
        {
            return _userManager.FindByEmailAsync(email).Result;
        }
        
        public async Task<AuthResponse> Authenticate(AuthRequest model)
        {
            var user = GetByEmail(model.Email);
            

            // return null if user not found
            if (user == null) return null;
            if (await _userManager.CheckPasswordAsync(user, model.Password)) return null;

                // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthResponse
            {
                Email = model.Email,
                Token = token
            };
        }


        private string generateJwtToken(IdentityUser user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("AdiletBolatbek26386EndProject.Net");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("email", user.Email) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}