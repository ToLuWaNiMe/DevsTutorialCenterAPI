using DevsTutorialCenterAPI.Models.DTOs;

namespace DevsTutorialCenterAPI.Services.Abstractions
{
    public interface IAuthService
    {
        Task<Result<AppUserDto>> Register(RegistrationRequestDTO registrationRequestDTO);
        Task<Result<LoginResponseDto>> Login(LoginRequestDto loginRequestDto);

        Task<bool> AssignRole(string email, string roleName);

        Task<AppUserDto> Register2(RegistrationRequestDTO registrationRequestDTO);
    }
}
