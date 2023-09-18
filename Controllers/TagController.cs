using DevsTutorialCenterAPI.Helpers;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers
{

    [Route("api/[tags]")]
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
            {
                var validationErrors = ModelStateErrorHelper.GetErrors(ModelState);

                return BadRequest(new ResponseDto<UpdateTagDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid model state.",
                    Error = validationErrors.ToString(),
                    Data = null
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
