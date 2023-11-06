using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers;

[Route("api/tenant-auth")]
[ApiController]
public class TenantAuthController : ControllerBase
{
    private readonly ITenantAuthService _authService;

    public TenantAuthController(ITenantAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("Login")]
    public async Task<IActionResult> LoginTenant([FromBody] LoginDto loginDto)
    {
        var response = await _authService.LoginTenantAsync(loginDto);
        return StatusCode(response.Code, response);
    }

    [HttpPost("register")]
    public async Task<ActionResult<ResponseDto<object>>> RegisterTenant([FromBody] RegisterTenantDto registerTenantDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(new ResponseDto<object>
                {
                    Data = null,
                    Code = 400,
                    Error = "Validation Failed",
                    Message = "Error"
                });
            var response = await _authService.RegisterTenantAsync(registerTenantDto);

            return Ok(new ResponseDto<object>
            {
                Data = response,
                Code = 200,
                Error = "",
                Message = "OK"
            });
        }
        catch (Exception ex)
        {
            //log error here ex.message
            return BadRequest(new ResponseDto<object>
            {
                Data = null,
                Code = 400,
                Error = "Failed to register tenant",
                Message = "Error"
            });
        }
    }
}