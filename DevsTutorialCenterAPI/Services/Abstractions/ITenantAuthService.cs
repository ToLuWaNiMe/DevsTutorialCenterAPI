using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions;

public interface ITenantAuthService
{
    Task<Tenant> RegisterTenantAsync(RegisterTenantDto registerTenantDto);
    Task<ResponseDto<object>> LoginTenantAsync(LoginDto loginDto);
    string GenerateJWT(string Tenant);
}