using AutoMapper;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevsTutorialCenterAPI.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly UserManager<AppUser> _userManager;
    private readonly IMapper _mapper;


    public AuthController(IAuthService authService, UserManager<AppUser> userManager, IMapper mapper)
    {
        _mapper = mapper;
        _userManager = userManager;
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

        //Add Token to verify the email
        var user = _mapper.Map<AppUser>(registerResult.Data);
        var appUrl =  $"{Request.Scheme}://{Request.Host}";
        var confirmEmailEndpoint = $"{appUrl}/confirmemail";
        var confirmationEmailSent = await _authService.SendConfirmationEmailAsync2(user, confirmEmailEndpoint);
        return Ok(ResponseDto<object>.Success(registerResult.Data));
    }

    [HttpPost("assign-role")]
    public async Task<IActionResult> GiveRole([FromBody] AssignRoleDTO model)
    {
        var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.RoleName);

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
            Data = assignRoleSuccessful,
            Error = "",
            Message = "OK",
            IsSuccessful = true
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
            Message = "OK",
            IsSuccessful = true
        });
    }

    [HttpGet("ConfirmEmail")]
    [AllowAnonymous]
    public async Task<IActionResult> ConfirmEmail(string email, string token)
    {
        // if email or token is null, end the process
        if (email == null || token == null)
        {
            return BadRequest(new
                { errorTitle = "Invalid Email or Token", errorMessage = "Email or token cannot be null" });
        }

        // ensure that user exists
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return NotFound(new { errorMessage = $"User with email {email} not found" });
        }

        // confirm email
        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (result.Succeeded)
        {
            return Ok(new { message = "Email confirmed successfully" });
        }

        // on failure
        return BadRequest(new { errorTitle = "Confirmation Failed", errorMessage = "Could not confirm email" });
    }

    [HttpPost("ForgotPassword")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordDto model)
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.SelectMany(x => x.Value.Errors.Select(xx => xx.ErrorMessage));
            var error = string.Join(" ", errors);
            return BadRequest(new { error = error });
        }

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return BadRequest(new { error = "Email does not exist" });
        }

        var appUrl =  $"{Request.Scheme}://{Request.Host}";
        var passwordResetEndpoint = $"{appUrl}/confirmemail";
        var passwordResetEmailSent =
            await _authService.SendPasswordResetEmailAsync(user, passwordResetEndpoint);

        if (passwordResetEmailSent)
        {
            return Ok(new
            {
                message =
                    "A reset password link has been sent to the email provided. Please go to your inbox and click on the link to reset your password"
            });
        }

        return BadRequest(new
        {
            message = "Failed to send a reset password link. Please try again"
        });
    }

    [HttpGet("ResetPassword")]
    public IActionResult ResetPassword(string token, string Email)
    {
        var viewModel = new ResetPasswordDto();
        if (string.IsNullOrEmpty(token))
        {
            return BadRequest(new { errorToken = "" });
        }

        if (string.IsNullOrEmpty(Email))
        {
            return BadRequest(new { errorEmail = "Email is required" });
        }

        viewModel.Token = token;
        viewModel.Email = Email;

        return Ok(viewModel);
    }


    [HttpPost("ResetPassword")]
    public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
                if (result.Succeeded)
                {
                    return Ok(new { message = "Password reset successfully" });
                }

                var errors = result.Errors.Select(err => new { code = err.Code, description = err.Description });
                return BadRequest(new { errors = errors });
            }
            else
            {
                return BadRequest(new { errorEmail = "Email not found" });
            }
        }

        return BadRequest(new { error = "Invalid model state" });
    }


    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user == null)
        {
            var response = new ResponseDto<object>
            {
                Code = (int)HttpStatusCode.NotFound,
                Data = null,
                Message = "User Not Found",
                Error = "User not found"
            };

            return NotFound(response);
        }

        var result = await _userManager.DeleteAsync(user);

        if (result.Succeeded)
        {
            var response = new ResponseDto<object>
            {
                Code = (int)HttpStatusCode.OK,
                Data = null,
                Message = "User Deleted Successfully",
                Error = string.Empty
            };

            return Ok(response);
        }
        else
        {
            var errors = result.Errors.Select(err => err.Description).ToList();
            var response = new ResponseDto<object>
            {
                Code = (int)HttpStatusCode.BadRequest,
                Data = null,
                Message = "Failed to Delete User",
                Error = string.Join(", ", errors)
            };

            return BadRequest(response);
        }
    }
}