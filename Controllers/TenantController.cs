using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories.interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantService _tenantService;

        public TenantController(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }


        [HttpDelete("delete/{identity}")]
        public async Task<IActionResult> DeleteTenant(string identity)
        {
            var response = await _tenantService.DeleteTenantAsync(identity);
            return StatusCode(response.Code, response);
        }
    }
}
