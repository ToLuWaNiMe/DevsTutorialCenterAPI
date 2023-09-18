using DevsTutorialCenterAPI.Helpers;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace DevsTutorialCenterAPI.Controllers
{

    [Route("api/[tags]")]
    [Route("api/tags")]
    [ApiController]
    public class TagController : ControllerBase
    {

        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<UpdateTagDto>>> UpdateTag([FromRoute] string id, [FromBody] UpdateTagDto updatedTag)
        {


            if (!ModelState.IsValid)
        [HttpGet("")]
        public async Task<ActionResult<ResponseDto<IEnumerable<GetAllTagsDto>>>> GetAllTagAsync()
            {
                var validationErrors = ModelStateErrorHelper.GetErrors(ModelState);
            var tags = await _tagService.GetAllTagAsync();

                return BadRequest(new ResponseDto<UpdateTagDto>
             return Ok(new ResponseDto<IEnumerable<GetAllTagsDto>>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid model state.",
                    Error = validationErrors.ToString(),
                    Data = null
                Code = 200,
                Message = "OK",
                Error = "",
                Data = tags
                });
            }

            var existingTag = await _tagService.UpdateAsync(id, updatedTag);

            return Ok(new ResponseDto<UpdateTagDto>
            {
                Code = StatusCodes.Status200OK,
                Message = "Tag updated successfully.",
                Data = existingTag
            });

        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<UpdateTagDto>>> DeleteTag([FromRoute] string id)
        {

            await _tagService.Delete(id);

            var response = new ResponseDto<UpdateTagDto>
            {
                Code = StatusCodes.Status200OK,
                Message = "Tag deleted successfully.",
                Data = null
            };

            return Ok(response);

    }
        }



    }
}
