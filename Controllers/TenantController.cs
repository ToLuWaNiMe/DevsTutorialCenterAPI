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


        [HttpDelete("delete/{Id}")]
        public async Task<IActionResult> DeleteTenant(string Id)
        {
            var response = await _tenantService.DeleteTenantAsync(Id);
            return StatusCode(response.Code, response);
        }
    }
}
