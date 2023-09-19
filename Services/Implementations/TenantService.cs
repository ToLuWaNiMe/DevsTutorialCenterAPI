using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories.interfaces;
using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Data.Repositories;

public class TenantService : ITenantService
{
    private readonly IRepository _repository;

    public TenantService(IRepository repository)
    {
        _repository = repository;
    }

    public async Task<ResponseDto<object>> DeleteTenantAsync(string Id)
    {
        try
        {
            // Validate identity and delete the tenant
            var tenant = await _repository.GetByIdAsync<Tenant>(Id);

            if (tenant != null)
            {
                await _repository.DeleteAsync(tenant);

                return new ResponseDto<object>
                {
                    Code = 200,
                    Message = "Ok",
                    Data = "",
                    Error = ""
                };
            }

            return new ResponseDto<object>
            {
                Code = 400,
                Message = "Error",
                Data = null,
                Error = "Tenant not found"
            };
        }
        catch (Exception ex)
        {
            return new ResponseDto<object>
            {
                Code = 400,
                Message = "Error",
                Data = null,
                Error = ex.Message // Include actual error message
            };
        }
    }
}