using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers;

[Route("api/tags")]
[ApiController]
public class TagController : ControllerBase
{
    private readonly ITagService _tagService;

    public TagController(ITagService tagService)
    {
        _tagService = tagService;
    }

    [HttpGet("get-all-tag")]
   // [Authorize]
    public async Task<ActionResult<ResponseDto<IEnumerable<GetAllTagsDto>>>> GetAllTagAsync()
    {
        var tag = await _tagService.GetAllTagAsync();

        return Ok(new ResponseDto<IEnumerable<GetAllTagsDto>>
        {
            Code = 200,
            Message = "OK",
            Error = "",
            Data = tag,
            IsSuccessful = true
        });
    }
    
    [HttpPost]
   // [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<ResponseDto<object>>> CreateTagAsync([FromBody] CreateTagDto createTagDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResponseDto<object>
            {
                Data = null,
                Code = 400,
                Error = "Invalid input data",
                Message = "Error"
            });
        }

        var tag = await _tagService.CreateTagAsync(createTagDto);

        return Ok(new ResponseDto<object>
        {
            Data = tag,
            Code = 200,
            Error = "",
            Message = "Ok",
            IsSuccessful = true

        } );

    }


    //[Authorize(Roles = "ADMIN")]
    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseDto<UpdateTagDto>>> UpdateTag([FromRoute] string id,
        [FromBody] UpdateTagDto updatedTag)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResponseDto<UpdateTagDto>
            {
                Code = StatusCodes.Status400BadRequest,
                Message = "Invalid model state.",
                Error = ModelState.GetError(),
                Data = null
            });
        }

        var existingTag = await _tagService.UpdateAsync(id, updatedTag);

        return Ok(new ResponseDto<UpdateTagDto>
        {
            Code = StatusCodes.Status200OK,
            Message = "Tag updated successfully.",
            Data = existingTag,
            IsSuccessful = true
        });
    }
}