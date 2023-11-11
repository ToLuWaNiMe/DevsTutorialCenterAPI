using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Services.Implementations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevsTutorialCenterAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(IUserService userService, SignInManager<AppUser> signInManager)
        {
            _userService = userService;
            _signInManager = signInManager;
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
                    Message = "Bad Request",
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
                    Code = (int)HttpStatusCode.BadRequest,
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


        [HttpGet("get-read-articles-by-user")]
        public async Task<ActionResult<List<GetReadArticlesDto>>> ArticlesReadByUser()
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);

            if (user == null)
            {
                // Handle the case where the user is not logged in
                return BadRequest(new ResponseDto<List<GetReadArticlesDto>>
                {
                    Data = null,
                    Code = (int)HttpStatusCode.BadRequest,
                    Message = "Bad Request",
                    Error = "User Not Found"
                });
            }

            var userId = user.Id;

            // Call the service method to get articles read by the user
            var readArticles = await _userService.GetArticleReadByUser(userId);

            return Ok(new ResponseDto<List<GetReadArticlesDto>>
            {
                Data = readArticles,
                Code = (int)HttpStatusCode.OK,
                Message = "Ok",
                Error = ""
            });
        }
    }
}

