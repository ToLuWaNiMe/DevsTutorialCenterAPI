using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace DevsTutorialCenterAPI.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagController : ControllerBase
    {

        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }


        [HttpGet("")]
        public async Task<ActionResult<ResponseDto<IEnumerable<GetAllTagsDto>>>> GetAllTagAsync()
        {
            var tags = await _tagService.GetAllTagAsync();
             return Ok(new ResponseDto<IEnumerable<GetAllTagsDto>>
            {
                Code = 200,
                Message = "OK",
                Error = "",
                Data = tags
            });


         }
    }
}
            

