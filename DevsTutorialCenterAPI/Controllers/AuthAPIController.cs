using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthAPIController : ControllerBase
    {
        private readonly IAuthService _authService;

        

        public AuthAPIController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]

        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO model)
        {
            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                return BadRequest(new ResponseDto<object>
                {
                    Data = null,
                    Code = 400,
                    Error = "Invalid input data",
                    Message = "Error"

                });
            }

            return Ok(new ResponseDto<object> 
            { 
                Code = 200,
                Data = "User Registered Successfully",
                Error= "",
                Message = "OK"
            
            });

        }

        [HttpPost("assign-role")]

        public async Task<IActionResult> GiveRole([FromBody] AssignRoleDTO model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.RoleName.ToUpper());

            if (!assignRoleSuccessful)
            {
                return BadRequest(new ResponseDto<object>
                {
                    Data = false,
                    Code = 400,
                    Error = "User not assigned role",
                    Message = "Error"

                });
            }

            return Ok(new ResponseDto<object>
            {
                Code = 200,
                Data = true,
                Error = "",
                Message = "OK"

            });
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
        {
            var loginResponse = await _authService.Login(model);

            if (loginResponse.User == null)
            {
                return BadRequest(new ResponseDto<object>
                {
                    Data = null,
                    Code = 400,
                    Error = "Username or Password incorrect",
                    Message = "Error"

                });
            }

            return Ok(new ResponseDto<object>
            {
                Code = 200,
                Data = loginResponse,
                Error = "",
                Message = "OK"

            });
        }
    }
}
