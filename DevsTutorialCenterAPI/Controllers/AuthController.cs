using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
    {
        var registerResult = await _authService.Register(model);

        if (registerResult.IsFailure)
        {
            return BadRequest(ResponseDto<object>.Failure(registerResult.Errors));
        }

        return Ok(ResponseDto<object>.Success(registerResult.Data));
    }

    [HttpPost("assign-role")]
    public async Task<IActionResult> GiveRole([FromBody] AssignRoleDTO model)
    {
        var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.RoleId);

        if (!assignRoleSuccessful)
        {
            return BadRequest(new ResponseDto<object>
            {
                Data = null,
                Code = 400,
                Error = "User not assigned role",
                Message = "Error"
            });
        }

        return Ok(new ResponseDto<object>
        {
            Code = 200,
            Data = null,
            Error = "",
            Message = "OK"
        });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
    {
        var loginResponse = await _authService.Login(model);
        if (loginResponse.IsFailure)
            return BadRequest(loginResponse.Errors);

        var res = ResponseDto<object>.Success(loginResponse.Data);
        return Ok(res);
    }

    [HttpGet("get-all-roles")]
    public async Task<IActionResult> GetRoles()
    {
        var result = await _authService.GetAllRoles();

        if (result == null)
        {
            return BadRequest(new ResponseDto<object>
            {
                Data = null,
                Code = 400,
                Error = "No roles found",
                Message = "Error"
            });
        }

        return Ok(new ResponseDto<object>
        {
            Code = 200,
            Data = result,
            Error = "",
            Message = "OK"
        });
    }
}