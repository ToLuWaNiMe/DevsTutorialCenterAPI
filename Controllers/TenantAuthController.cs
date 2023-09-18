using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantController : ControllerBase
    {
        private readonly ITenantAuthService _tenantService;

        public TenantController(ITenantAuthService tenantService)
        {
            _tenantService = tenantService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseDto<object>>> RegisterTenant([FromBody] RegisterTenantDto registerTenantDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseDto<object>
                    {

                        Data = null,
                        Code = 400,
                        Error = "Validation Failed",
                        Message = "Error"
                    });
                }
                var response = await _tenantService.RegisterTenantAsync(registerTenantDto);

                return Ok(new ResponseDto<object>
                {

                    Data = response,
                    Code = 200,
                    Error = "",
                    Message = "OK"
                });
            }
            catch (Exception ex)
            {
                //log error here ex.message
                return BadRequest(new ResponseDto<object>
                {

                    Data = null,
                    Code = 400,
                    Error = "Failed to register tenant",
                    Message = "Error"
                });
            } 
        }
    }
}
