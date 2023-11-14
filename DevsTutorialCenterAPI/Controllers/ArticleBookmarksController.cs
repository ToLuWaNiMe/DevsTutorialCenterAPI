using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly SignInManager<AppUser> _signInManager;

        public ArticleBookmarksController(IBookmarkService bookmarkService, IRepository repository, SignInManager<AppUser> signInManager)
        {
            _bookmarkService = bookmarkService;
            _repository = repository;
            _signInManager = signInManager;
        }

        [HttpPost("bookmark/{articleId}")]
        public async Task<IActionResult> BookmarkArticle(string articleId)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);

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
           var Bookmark = await _bookmarkService.BookmarkArticleAsync(articleId, user.Id); 

            var response = new ResponseDto<ArticlesBookmarkDto>
            {
                Code = 200,
                Message = "Article bookmarked successfully",
                Data = Bookmark,
                Error = string.Empty
            };

            return Ok(response);
        }

        [HttpDelete("unbookmark/{articleId}")]
        public async Task<IActionResult> UnbookmarkArticle(string articleId)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);
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
            await _bookmarkService.UnbookmarkArticleAsync(articleId, user.Id);

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
