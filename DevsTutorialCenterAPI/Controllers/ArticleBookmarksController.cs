using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DevsTutorialCenterAPI.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/article-bookmarks")]
    public class ArticleBookmarksController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;
        private readonly IRepository _repository;

        public ArticleBookmarksController(IBookmarkService bookmarkService, IRepository repository)
        {
            _bookmarkService = bookmarkService;
            _repository = repository;
        }

        [HttpPost("bookmark")]
        public async Task<IActionResult> BookmarkArticle([FromBody] ArticlesBookmarkDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDto<object>
                {
                    Data = null,
                    Code = 500,
                    Error = "Invalid data",
                    Message = "Failed"
                });
            }

            // Proceed to bookmark the article if the user and article exist
           var Bookmark = await _bookmarkService.BookmarkArticleAsync(dto); 

            var response = new ResponseDto<ArticlesBookmarkDto>
            {
                Code = 200,
                Message = "Article bookmarked successfully",
                Data = Bookmark,
                Error = string.Empty
            };

            return Ok(response);
        }

        [HttpDelete("unbookmark")]
        public async Task<IActionResult> UnbookmarkArticle([FromBody] ArticlesBookmarkDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ResponseDto<object>
                {
                    Data = null,
                    Code = 500,
                    Error = "Invalid data",
                    Message = "Failed"
                });
            }

            // Proceed to unbookmark the article if the user and article exist
            await _bookmarkService.UnbookmarkArticleAsync(dto.ArticleId, dto.UserId);

            var response = new ResponseDto<string>
            {
                Code = 200,
                Message = "Article Unbookmarked successfully",
                Error = string.Empty
            };

            return Ok(response);
        }
    }
}
