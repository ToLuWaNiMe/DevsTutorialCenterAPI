using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace DevsTutorialCenterAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {

        private readonly ITagService _tagService;

        public TagController(ITagService tagService)
        {
            _tagService = tagService;
        }


        [HttpGet ("getalltags")]
        public async Task<ActionResult<ResponseDto<IEnumerable<GetAllTagsDto>>>> GetAllTagAsync()
        {
            var tags = await _tagService.GetAllTagAsync();
            if(!tags.Any())
            {
              
           return NotFound(new ResponseDto<IEnumerable<GetAllTagsDto>>
           {
               Code =404,
               Message = "Not found",
               Error = "",
               Data = tags
           });
            }

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
            

