using DevsTutorialCenterAPI.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IAuthService
    {
        Task<Result<AppUserDto>> Register(RegistrationRequestDTO registrationRequestDTO);
        Task<Result<LoginResponseDto>> Login(LoginRequestDto loginRequestDto);

        Task<bool> AssignRole(string email, string roleId);

        Task<AppUserDTO> Register2(RegistrationRequestDTO registrationRequestDTO);

        Task<List<IdentityRole>> GetAllRoles();
    }
}
