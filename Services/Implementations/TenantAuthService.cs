using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories.interfaces;
using DevsTutorialCenterAPI.Models.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevsTutorialCenterAPI.Data.Repositories
{
    public class TenantAuthService : ITenantAuthService
    {
        private readonly IConfiguration _config;
        private readonly IRepository repository;
        private readonly Helper _passwordHasher;

        public TenantAuthService(IConfiguration config, IRepository repository, Helper passwordHasher)
        {
            _config = config;
            this.repository = repository;
            _passwordHasher = passwordHasher;
        }

        public string GenerateJWT(string tenant)
        {

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);
            var securityToken = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddDays(Convert.ToInt32(_config.GetSection("JWT:LifeSpan").Value)),
                signingCredentials: signingCredentials
            );
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);
            return token;
        }



        public Task<ResponseDto<object>> LoginTenantAsync(LoginDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Identity) || string.IsNullOrEmpty(loginDto.Password))
            {
                return Task.FromResult (new ResponseDto<object>
                {
                    Code = 400,
                    Message = "Error",
                    Data = "",
                    Error = "Invalid Login Request"
                });
            }


            var jwtToken = GenerateJWT(loginDto.Identity);

            if (jwtToken != null)
            {
                return Task.FromResult(new ResponseDto<object>
                {
                    Code = 200,
                    Message = "Ok",
                    Data = jwtToken,
                    Error = ""
                });
            }
            return Task.FromResult(new ResponseDto<object>
            {
                Code = 400,
                Message = "Error",
                Data = "",
                Error = "Invalid credentials"
            });
        }
    }
}