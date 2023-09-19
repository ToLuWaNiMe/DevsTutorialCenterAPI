using System.IdentityModel.Tokens.Jwt;
using System.Text;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Utilities;
using Microsoft.IdentityModel.Tokens;

namespace DevsTutorialCenterAPI.Data.Repositories;

public class TenantAuthService : ITenantAuthService
{
    private readonly IConfiguration _config;
    private readonly IRepository _repository;

    public TenantAuthService(IConfiguration config, IRepository repository)
    {
        _config = config;
        _repository = repository;
    }

    public string GenerateJWT(string tenant)
    {
        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value);
        var signingCredentials =
            new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature);
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
            return Task.FromResult(new ResponseDto<object>
            {
                Code = 400,
                Message = "Error",
                Data = "",
                Error = "Invalid Login Request"
            });


        var jwtToken = GenerateJWT(loginDto.Identity);

        if (jwtToken != null)
            return Task.FromResult(new ResponseDto<object>
            {
                Code = 200,
                Message = "Ok",
                Data = jwtToken,
                Error = ""
            });
        return Task.FromResult(new ResponseDto<object>
        {
            Code = 400,
            Message = "Error",
            Data = "",
            Error = "Invalid credentials"
        });
    }

    public async Task<Tenant> RegisterTenantAsync(RegisterTenantDto registerTenantDto)
    {
        var newTenant = new Tenant
        {
            Name = registerTenantDto.Name,
            Identity = registerTenantDto.Identity,
            Password = Helper.HashPassword(registerTenantDto.Password)
        };
        // Hash the password before storing it


        await _repository.AddAsync(newTenant);

        return newTenant;
    }
}