using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers
{
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
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDto<object>
                {
                    Data = null,
                    Code = 500,
                    Error = "Invalid input data",
                    Message = "Error"

                });
            }

            var newUser = await _authService.Register2(model);
           

            return Ok(new ResponseDto<object> 
            { 
                Code = 200,
                Data = newUser,
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
