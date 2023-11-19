using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IAuthService
    {
        Task<Result<AppUserDTO>> Register(RegistrationRequestDTO registrationRequestDTO);
        Task<Result<LoginResponseDTO>> Login(LoginRequestDTO loginRequestDTO);

        Task<bool> AssignRole(string email, string roleName);

        Task<AppUserDTO> Register2(RegistrationRequestDTO registrationRequestDTO);
    }
}
