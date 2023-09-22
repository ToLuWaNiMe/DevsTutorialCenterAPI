using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Data.Repositories.interfaces;

public interface ITenantService
{
    Task<ResponseDto<object>> DeleteTenantAsync(string Id);
}