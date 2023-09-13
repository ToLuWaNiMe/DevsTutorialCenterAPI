using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService tenantService;

        public TenantController(ITenantService _tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterTenant([FromBody] Tenant tenant)
        {
            var response = await tenantService.RegisterTenantAsync(tenant);
            return StatusCode(response.Code, response);
        }
    }
}
