using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace DevsTutorialCenterAPI.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly TagService _tagService;

        public TagController(TagService tagService)
        {
            _tagService = tagService;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDto<IEnumerable<GetAllTagsDto>>>> GetAllTagAsync(IEnumerable<GetAllTagsDto> tags)
        {
            var tag = await _tagService.GetAllTagAsync();

            return Ok(new ResponseDto<IEnumerable<GetAllTagsDto>>
            {
                Code = 200,
                Message = "OK",
                Error = "",
                Data = tags,
            });
                

        }
    }
}
