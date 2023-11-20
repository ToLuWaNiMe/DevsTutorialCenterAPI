using DevsTutorialCenterAPI.Data;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Identity;

namespace DevsTutorialCenterAPI.Services.Implementations;

public class AuthService : IAuthService
{
    private readonly DevsTutorialCenterAPIContext _devs;
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IJwtTokenGeneratorService _jwtTokenGenerator;

    public AuthService(
        DevsTutorialCenterAPIContext devs,
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IJwtTokenGeneratorService jwtTokenGenerator)
    {
        _devs = devs;
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<bool> AssignRole(string email, string roleName)
    {
        var user = _devs.AppUsers.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        if (user == null) return false;
        
        if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
        {
            //create role if it does not exist
            _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
        }

        await _userManager.AddToRoleAsync(user, roleName);
        return true;
    }

    public async Task<Result<LoginResponseDto>> Login(LoginRequestDto loginRequestDto)
    {
        var user = _devs.AppUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDto.Email.ToLower());
        if (user is null)
            return Result.Failure<LoginResponseDto>(new[]
                { new Error("Auth.Error", "username or password not correct") });

        var isValid = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);

        if (!isValid)
            return Result.Failure<LoginResponseDto>(new[]
                { new Error("Auth.Error", "username or password not correct") });

        //If user is found, generate JWT Token
        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenGenerator.GenerateToken(user, roles);

        var appUserDto = new AppUserDto
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Squad = user.Squad,
            Stack = user.Stack
        };
        
        var loginResponseDto = new LoginResponseDto()
        {
            User = appUserDto,
            Token = token
        };

        return Result.Success(loginResponseDto);
    }

    public async Task<Result<AppUserDto>> Register(RegistrationRequestDTO registrationRequestDTO)
    {
        AppUser user = new()
        {
            UserName = registrationRequestDTO.Email,
            Email = registrationRequestDTO.Email,
            NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
            FirstName = registrationRequestDTO.FirstName,
            LastName = registrationRequestDTO.LastName,
            PhoneNumber = registrationRequestDTO.PhoneNumber,
            Stack = registrationRequestDTO.Stack,
            Squad = registrationRequestDTO.Squad
        };
            
        var result = await _userManager.CreateAsync(user, registrationRequestDTO.Password);

        if (!result.Succeeded)
            return Result.Failure<AppUserDto>(result.Errors.Select(e => new Error(e.Code, e.Description)));
            
        var userToReturn = _devs.AppUsers.First(u => u.UserName == registrationRequestDTO.Email);

        AppUserDto appUserDTO = new()
        {
            Id = userToReturn.Id,
            Email = userToReturn.Email,
            FirstName = userToReturn.FirstName,
            LastName = userToReturn.LastName,
            PhoneNumber = userToReturn.PhoneNumber,
            Squad = userToReturn.Squad,
            Stack = userToReturn.Stack
        };

        return Result.Success(appUserDTO);
    }

    //public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
    //{
    //    var user = _devs.AppUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());

    //    bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

    //    if (user == null || isValid == false)
    //    {
    //        return new LoginResponseDTO()
    //        {
    //            User = null,
    //            Token = ""
    //        };
    //    }

    //    //If user is found, generate JWT Token
    //    var roles = await _userManager.GetRolesAsync(user);
    //    var token = _jwtTokenGenerator.GenerateToken(user, roles);

    //    AppUserDTO appUserDTO = new()
    //    {
    //        Email = user.Email,
    //        ID = user.Id,
    //        Name = user.Name,
    //        PhoneNumber = user.PhoneNumber,
    //        Squad = user.Squad,
    //        Stack = user.Stack
    //    };
    //    LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
    //    {
    //        User = appUserDTO,
    //        Token = token
    //    };

    //    return loginResponseDTO;
    //}

    public async Task<AppUserDto> Register2(RegistrationRequestDTO registrationRequestDTO)
    {
        AppUser user = new()
        {
            UserName = registrationRequestDTO.Email,
            Email = registrationRequestDTO.Email,
            NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
            FirstName = registrationRequestDTO.FirstName,
            LastName = registrationRequestDTO.LastName,
            PhoneNumber = registrationRequestDTO.PhoneNumber,
            Stack = registrationRequestDTO.Stack,
            Squad = registrationRequestDTO.Squad
        };
            
        var result = await _userManager.CreateAsync(user, registrationRequestDTO.Password);

        if (!result.Succeeded) return null;
            
        var userToReturn = _devs.AppUsers.First(u => u.UserName == registrationRequestDTO.Email);

        AppUserDto appUserDTO = new()
        {
            Id = userToReturn.Id,
            Email = userToReturn.Email,
            FirstName = userToReturn.FirstName,
            LastName = userToReturn.LastName,
            PhoneNumber = userToReturn.PhoneNumber,
            Squad = userToReturn.Squad,
            Stack = userToReturn.Stack
        };

        return appUserDTO;
    }
}