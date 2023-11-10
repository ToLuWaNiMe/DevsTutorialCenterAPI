using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevsTutorialCenterAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Roles = "ADMIN")]
    public class UserController : ControllerBase
    {
        private readonly IUserManagementService _userService;

        public UserController(IUserManagementService userService)
        {
            _userService = userService;
        }

        [HttpGet("get-all-users")]
        public async Task<ActionResult<IEnumerable<object>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            if (users == null)
            {
                return BadRequest(new ResponseDto<object>
                {
                    Data = null,
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "User may not exist or has been deleted",
                    Error = "No Users Found"
                });
            }
            return Ok(new ResponseDto<object>
            {
                Data = users,
                Code = (int)HttpStatusCode.OK,
                Message = "Ok",
                Error = ""
            });
        }


        [HttpGet("get-user-by-id/{userId}")]
        public async Task<ActionResult<object>> GetUserById(string userId)
        {
            var users = await _userService.GetUserById(userId);

            if (users == null)
            {
                return BadRequest(new ResponseDto<object>
                {
                    Data = null,
                    Code = (int)HttpStatusCode.NotFound,
                    Message = "Bad Request",
                    Error = "User Not Found"
                });
            }
            return Ok(new ResponseDto<object>
            {
                Data = users,
                Code = (int)HttpStatusCode.OK,
                Message = "Ok",
                Error = ""
            });
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
                return NotFound("User not found");

            if (!await _userService.SoftDeleteUser(id))
                return BadRequest("User does not exist or has been deleted already");

            return Ok("User deleted!");
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(
            string id,
            [FromBody] AppUserUpdateRequestDTO appUserDTO)
        {
            if (!await _userService.UpdateUser(id, appUserDTO))
                return BadRequest();

            return Ok("User Updated!");
        }
    }
}
