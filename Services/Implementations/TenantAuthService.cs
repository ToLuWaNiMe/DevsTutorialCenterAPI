using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;

namespace DevsTutorialCenterAPI.Services.Implementations
{
    public class TenantAuthService : ITenantAuthService
    {
        private readonly IRepository _repository;
        //private readonly IConfiguration _config;
        private readonly Helper _passwordHasher;

        public TenantAuthService(IRepository repository, IConfiguration config, Helper passwordHasher)
        {
            _repository = repository;
            //_config = config;
            _passwordHasher = passwordHasher; // Inject PasswordHasher
        }


        public async Task<Tenant> RegisterTenantAsync(RegisterTenantDto registerTenantDto)
        {
           
            var newTenant = new Tenant()
            {
                Name = registerTenantDto.Name,
                Identity = registerTenantDto.Identity,
                Password = _passwordHasher.HashPassword(registerTenantDto.Password),
            };
            // Hash the password before storing it
                    

            await _repository.AddAsync<Tenant>(newTenant);

            return newTenant;
            
        }
    }
}

