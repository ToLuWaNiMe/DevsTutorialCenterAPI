using DevsTutorialCenterAPI.Helpers;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Interfaces;
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

        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<TagDto>>> UpdateTag([FromRoute] string id, [FromBody] TagDto updatedTag)
        {


            if (!ModelState.IsValid)
            {
                var validationErrors = ModelStateErrorHelper.GetErrors(ModelState);

                return BadRequest(new ResponseDto<TagDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid model state.",
                    Error = validationErrors.ToString(),
                    Data = null
                });
            }

            var existingTag = await _tagService.UpdateAsync(id, updatedTag);

            return Ok(new ResponseDto<TagDto>
            {
                Code = StatusCodes.Status200OK,
                Message = "Tag updated successfully.",
                Data = existingTag
            });

        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<TagDto>>> DeleteTag(string id)
        {

            if (!ModelState.IsValid)
            {
                var validationErrors = ModelStateErrorHelper.GetErrors(ModelState);
                return BadRequest(new ResponseDto<TagDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid model state.",
                    Error = validationErrors.ToString(),
                    Data = null
                });
            }

            await _tagService.Delete(id);

            var response = new ResponseDto<TagDto>
            {
                Code = StatusCodes.Status200OK,
                Message = "Tag deleted successfully.",
                Data = null
            };

            return Ok(response);

        }



    }
}
