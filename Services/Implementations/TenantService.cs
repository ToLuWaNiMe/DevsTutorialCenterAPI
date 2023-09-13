using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    public class TenantService : ITenantService
    {
        private readonly IRepository _repository;
        //private readonly IConfiguration _config;
        private readonly Helper _passwordHasher;

        public TenantService(IRepository repository, IConfiguration config, Helper passwordHasher)
        {
            _repository = repository;
            //_config = config;
            _passwordHasher = passwordHasher; // Inject PasswordHasher
        }


        public async Task<ResponseDto<object>> RegisterTenantAsync(Tenant tenant)
        {
            try
            {
                if (tenant != null)
                {
                    // Hash the password before storing it
                    tenant.Password = _passwordHasher.HashPassword(tenant.Password);

                    await _repository.AddAsync(tenant);

                    return new ResponseDto<object>
                    {
                        Code = 200,
                        Message = "Ok",
                        Data = new { tenantId = tenant.Identity },
                        Error = ""
                    };
                }
                else
                {
                    return new ResponseDto<object>
                    {
                        Code = 400,
                        Message = "Error",
                        Data = "",
                        Error = "Invalid try again"
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Code = 400,
                    Message = "Error",
                    Data = "",
                    Error = "An error occurred: " + ex.Message
                };
            }
        }
    }
}

