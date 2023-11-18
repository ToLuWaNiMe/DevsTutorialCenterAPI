using DevsTutorialCenterAPI.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDTO registrationRequestDTO);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);

        Task<bool> AssignRole(string email, string roleId);

        Task<AppUserDTO> Register2(RegistrationRequestDTO registrationRequestDTO);

        Task<List<IdentityRole>> GetAllRoles();
    }
}
