using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Data.Repositories.interfaces
{
    public interface ITenantAuthService
    {
        Task<ResponseDto<object>> LoginTenantAsync(LoginDto loginDto);
        string GenerateJWT(string Tenant);
    }
}
