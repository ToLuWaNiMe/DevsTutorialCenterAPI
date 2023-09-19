using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Utilities;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    public class TenantAuthService : ITenantAuthService
    {
        private readonly IRepository _repository;

        public TenantAuthService(IRepository repository)
        {
            _repository = repository;
        }


        public async Task<Tenant> RegisterTenantAsync(RegisterTenantDto registerTenantDto)
        {
           
            var newTenant = new Tenant()
            {
                Name = registerTenantDto.Name,
                Identity = registerTenantDto.Identity,
                Password = Helper.HashPassword(registerTenantDto.Password),
            };
            // Hash the password before storing it
                    

            await _repository.AddAsync<Tenant>(newTenant);

            return newTenant;
            
        }
    }
}

