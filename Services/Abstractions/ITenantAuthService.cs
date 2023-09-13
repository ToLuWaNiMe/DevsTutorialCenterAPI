using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface ITenantAuthService
    {
        Task<object> RegisterTenantAsync(RegisterTenantDto registerTenantDto);
    }
}
