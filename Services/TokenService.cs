using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AduabaNeptune.Helper;
using AduabaNeptune.Model;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AduabaNeptune.Services
{
    public class TokenService : ITokenService
    {
        private readonly JWT _jwtSettings;

        public TokenService(IOptions<JWT> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }
        
        public string GenerateToken(Customer customer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            //key used for sigingCredentials
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", customer.Id.ToString()),
                    new Claim("Email", customer.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                
            };

            //Create token using tokenDescriptor
            var createToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(createToken);

            return token;
        }
    }
}