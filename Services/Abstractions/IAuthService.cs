using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Data.Repositories.interfaces
{
    public interface IAuthService
    {
        Task<ResponseDto<object>> LoginTenantAsync(string identity, string password);
        string GenerateJWT(string Tenant);
        //Task<ResponseDto<object>> LoginTenantAsync(string identity, string password);
        //Task<ResponseDto<object>> DeleteTenantAsync(string identity);
    }
}
