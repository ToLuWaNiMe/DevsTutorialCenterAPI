using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories.interfaces;
using DevsTutorialCenterAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        //private readonly UserManager<AppUser> _userManager;
        //private readonly IMapper _mapper;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
            //_userManager = userManager;
            //_mapper = mapper;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> LoginTenant([FromBody] Tenant request)
        {
            var response = await _authService.LoginTenantAsync(request.Identity, request.Password);
            return StatusCode(response.Code, response);
        }
    }
}
