using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers
{
    [Route("api/[tag]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }
        [Authorize]
        [HttpPost("create-tag")]
        public async Task<ActionResult<ResponseDto<object>>> CreateTagAsync([FromBody] CreateTagDto createTagDto)
        {
            if (ModelState.IsValid)
            {
                return BadRequest(new ResponseDto<object>
                {
                    Data = null,
                    Code = 500,
                    Error = "Validation failed",
                    Message = "Error",
                });
            }

            await _tagService.CreateTagAsync(createTagDto);
            return Ok(new ResponseDto<object>
            {
                Code = 200,
                Message = "OK",
                Error = "",
                Data = createTagDto,
            });


        }
    }
}
