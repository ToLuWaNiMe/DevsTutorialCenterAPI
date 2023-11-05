using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    public class JwtTokenGeneratorService : IJwtTokenGeneratorService
    {
        private readonly IConfiguration _config;

        public JwtTokenGeneratorService(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(AppUser appUser, IEnumerable<string> roles)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value);

            List<Claim> claimList = new()
            {
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim(JwtRegisteredClaimNames.Sub, appUser.Id),
                new Claim(JwtRegisteredClaimNames.Name, appUser.UserName)
            };

            claimList.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = _config.GetSection("JWT:Audience").Value,
                Issuer = _config.GetSection("JWT:Issuer").Value,
                Subject = new ClaimsIdentity(claimList),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
