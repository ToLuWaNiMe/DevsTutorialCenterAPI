using DevsTutorialCenterAPI.Data.Repositories.interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers;

[Route("api/tenant")]
[ApiController]
public class TenantController : ControllerBase
{
    private readonly ITenantService _tenantService;

    public TenantController(ITenantService tenantService)
    {
        _tenantService = tenantService;
    }

    [Authorize(Roles = "admin")]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteTenant(string id)
    {
        var response = await _tenantService.DeleteTenantAsync(id);
        return StatusCode(response.Code, response);
    }
}