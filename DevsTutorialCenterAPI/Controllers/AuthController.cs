﻿using DevsTutorialCenterAPI.Models.DTOs;
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
        var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.RoleName.ToUpper());

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
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
    {
        var loginResponse = await _authService.Login(model);
        if (loginResponse.IsFailure)
            return BadRequest(loginResponse.Errors);

        return Ok(ResponseDto<object>.Success(loginResponse));
    }
}