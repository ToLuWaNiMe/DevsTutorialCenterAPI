using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using Microsoft.AspNetCore.Identity;

namespace DevsTutorialCenterAPI.Services.Abstractions;

public interface IAuthService
{
    Task<Result<AppUserDto>> Register(RegistrationRequestDTO registrationRequestDTO);
    Task<Result<LoginResponseDto>> Login(LoginRequestDto loginRequestDto);

    Task<bool> AssignRole(string email, string roleId);

    Task<AppUserDto> Register2(RegistrationRequestDTO registrationRequestDTO);

    Task<List<IdentityRole>> GetAllRoles();

    Task<bool> SendConfirmationEmailAsync(AppUser user, string confirmEmailAddress);

    Task<bool> SendPasswordResetEmailAsync(AppUser user, string resetPasswordAction);
    Task<bool> SendConfirmationEmailAsync2(AppUser user, string confirmEmailAddress);

}