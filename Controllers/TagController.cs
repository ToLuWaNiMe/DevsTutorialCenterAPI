using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
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


        [HttpGet]
        public async Task<ActionResult<ResponseDto<IEnumerable<Tag>>>> GetAllTagAsync(int page = 1, int pageSize = 10)
        {
            var tags = await _tagService.GetAllTagAsync();

            // Calculate the total number of items and pages
            int totalItems = tags.Count();


            // Calculate the number of items to skip and take for the current page
            int skip = (page - 1) * pageSize;
            int take = pageSize;

            // Retrieve the paged tags
            var pagedTags = tags.Skip(skip).Take(take).ToList();

            return Ok(new ResponseDto<IEnumerable<Tag>>
            {
                Code = 200,
                Message = "OK",
                Error = "",
                Data = pagedTags,


            });
        }
    }
}
