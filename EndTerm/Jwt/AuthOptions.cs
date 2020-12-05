using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace EndTerm.Jwt
{
    public class AuthOptions
    {
        public const string ISSUER = "localhoast:5001";
        public const string AUDIENCE = "User"; 
        const string KEY = "AdiletBolatbek26386endtermproject";  
        public const int LIFETIME = 60; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}