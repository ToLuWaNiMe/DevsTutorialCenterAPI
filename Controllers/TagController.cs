using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<ResponseDto<object>>> CreateTagAsync(CreateTagDto createTagDto)
        {
            if (createTagDto == null)
            {
                return BadRequest(new ResponseDto<object>
                {
                    Data = null,
                    Code = 500,
                    Error = "Failed to add tag",
                    Message = "Error",
                });
            }

            var tag = new Tag { CreatedOn = DateTime.UtcNow, Name = createTagDto.Name };


            await _tagService.CreateTagAsync(tag);
            return Ok(new ResponseDto<object>
            {
                Code = 200,
                Message = "OK",
                Error = "",
                Data = tag,
            });


        }
    }
}
