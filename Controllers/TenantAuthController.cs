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
    public class TenantAuthController : ControllerBase
    {
        private readonly ITenantAuthService _authService;
        //private readonly UserManager<AppUser> _userManager;
        //private readonly IMapper _mapper;
        public TenantAuthController(ITenantAuthService authService)
        {
            _authService = authService;
            //_userManager = userManager;
            //_mapper = mapper;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> LoginTenant([FromBody] LoginDto loginDto)
        {
            var response = await _authService.LoginTenantAsync(loginDto);
            return StatusCode(response.Code, response);
        }
    }
}
