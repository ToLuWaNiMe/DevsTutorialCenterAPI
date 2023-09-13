using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<ResponseDto<TagDto>>> UpdateTag(string id, [FromBody] TagDto updatedTag)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new ResponseDto<TagDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid ID.",
                    Data = null
                });
            }

            if (updatedTag == null)
            {
                return BadRequest(new ResponseDto<TagDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid model data.",
                    Data = null
                });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDto<TagDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid model state.",
                    Data = null
                });
            }

            try
            {
                var existingTag = await _tagService.UpdateAsync(id, updatedTag);

                if (existingTag == null)
                {
                    return NotFound(new ResponseDto<TagDto>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Tag not found.",
                        Data = null
                    });
                }

                // You can choose to return the updated tag here if neede
                var updatedTagDto = new TagDto
                {
                    Id = existingTag.Id,
                    Name = existingTag.Name,
                    // Map other properties as needed
                };

                return Ok(new ResponseDto<TagDto>
                {
                    Code = StatusCodes.Status200OK,
                    Message = "Tag updated successfully.",
                    Data = updatedTagDto
                });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<TagDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Error = ex.Message,
                    Message = "Error updating the tag.",
                    Data = null
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<TagDto>>> DeleteTag(string id)
        {
            try
            {
                var existingTag = await _tagService.GetByIdAsync<Tag>(id);

                if (existingTag == null)
                {
                    return NotFound(new ResponseDto<TagDto>
                    {
                        Code = StatusCodes.Status404NotFound,
                        Message = "Tag not found.",
                        Data = null
                    });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseDto<TagDto>
                    {
                        Code = StatusCodes.Status400BadRequest,
                        Message = "Invalid model state.",
                        Data = null
                    });
                }

                await _tagService.Delete(id);

                return Ok("Tag Deleted");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseDto<TagDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Error = ex.Message,
                    Message = "Error deleting the tag.",
                    Data = null
                });
            }
        }


    }
}
