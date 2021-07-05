using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AduabaNeptune.Helper;
using AduabaNeptune.Data.Entities;
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
        
        public string GenerateCustomerToken(Customer customer)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            //key used for sigingCredentials
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(CustomClaimType.Id.ToString(), customer.Id.ToString()),
                    new Claim(CustomClaimType.Email.ToString(), customer.Email),
                    new Claim(CustomClaimType.Lastname.ToString(), customer.LastName),
                    new Claim(CustomClaimType.Firstname.ToString(), customer.FirstName),
                    new Claim(CustomClaimType.ImageUrl.ToString(), customer.AvatarUrl),
                    new Claim(CustomClaimType.PhoneNumber.ToString(), customer.PhoneNumber),
                    new Claim(CustomClaimType.Role.ToString(), customer.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                
            };

            //Create token using tokenDescriptor
            var createToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(createToken);

            return token;
        }

        public string GenerateEmployeeToken(Employee employee)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            //key used for sigingCredentials
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(CustomClaimType.Id.ToString(), employee.Id.ToString()),
                    new Claim(CustomClaimType.Email.ToString(), employee.OfficialEmail),
                    new Claim(CustomClaimType.Lastname.ToString(), employee.LastName),
                    new Claim(CustomClaimType.Firstname.ToString(), employee.FirstName),
                    new Claim(CustomClaimType.ImageUrl.ToString(), employee.AvatarUrl),
                    new Claim(CustomClaimType.PhoneNumber.ToString(), employee.PhoneNumber),
                    new Claim(CustomClaimType.Role.ToString(), employee.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                
            };

            //Create token using tokenDescriptor
            var createToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(createToken);

            return token;
        }

        public string GenerateVendorToken(Vendor vendor)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            
            //key used for sigingCredentials
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(CustomClaimType.Id.ToString(), vendor.Id.ToString()),
                    new Claim(CustomClaimType.Email.ToString(), vendor.Email),
                    new Claim(CustomClaimType.Firstname.ToString(), vendor.FirstName),
                    new Claim(CustomClaimType.Lastname.ToString(), vendor.LastName),
                    new Claim(CustomClaimType.ShopName.ToString(), vendor.ShopName),
                    new Claim(CustomClaimType.ImageUrl.ToString(), vendor.ShopLogoUrl),
                    new Claim(CustomClaimType.PhoneNumber.ToString(), vendor.PhoneNumber),
                    new Claim(CustomClaimType.Role.ToString(), vendor.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                
            };

            //Create token using tokenDescriptor
            var createToken = tokenHandler.CreateToken(tokenDescriptor);
            var token = tokenHandler.WriteToken(createToken);

            return token;
        }
    }
}