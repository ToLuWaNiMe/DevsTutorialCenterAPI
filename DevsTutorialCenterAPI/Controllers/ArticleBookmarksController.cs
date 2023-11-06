using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Interfaces;
using DevsTutorialCenterAPI.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DevsTutorialCenterAPI.Controllers
{
    [Authorize]
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

        [HttpPost("bookmark/{articleId}")]
        public async Task<IActionResult> BookmarkArticle(string articleId, [FromBody] ArticlesBookmarkDto dto)
        {
            try
            {
                string userId = dto.UserId;

                // Check if the user exists
                var user = await _repository.GetByIdAsync<AppUser>(userId);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                // Check if the article exists by Id
                var article = await _repository.GetByIdAsync<Article>(articleId);
                if (article == null)
                {
                    return NotFound("Article not found");
                }

                // Proceed to bookmark the article if the user and article exist
                await _bookmarkService.BookmarkArticleAsync(articleId, userId);

                // Return a response indicating success
                var response = new ResponseDto<string>
                {
                    Code = 200,
                    Message = "Article bookmarked successfully",
                    Data = "Article bookmarked successfully",
                    Error = string.Empty
                };

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                var response = new ResponseDto<string>
                {
                    Code = 404, // Not Found
                    Message = ex.Message,
                    Data = string.Empty,
                    Error = ex.Message
                };

                return NotFound(response);
            }
            catch (InvalidOperationException ex)
            {
                var response = new ResponseDto<string>
                {
                    Code = 400, // Bad Request
                    Message = ex.Message,
                    Data = string.Empty,
                    Error = ex.Message
                };

                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseDto<string>
                {
                    Code = 500, // Internal Server Error
                    Message = "Error bookmarking the article",
                    Data = string.Empty,
                    Error = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        [HttpDelete("remove-bookmark/{articleId}")]
        public async Task<IActionResult> RemoveBookmark(string articleId, [FromBody] ArticlesBookmarkDto dto)
        {
            try
            {
                string userId = dto.UserId;

                // Check if the user exists
                var user = await _repository.GetByIdAsync<AppUser>(userId);
                if (user == null)
                {
                    return NotFound("User not found");
                }

                // Check if the article exists by Id
                var article = await _repository.GetByIdAsync<Article>(articleId);
                if (article == null)
                {
                    return NotFound("Article not found");
                }

                // Check if the user has bookmarked the article
                var bookmark = await _repository.GetAllAsync<ArticleBookMark>();
                var existingBookmark = bookmark.FirstOrDefault(ab => ab.ArticleId == articleId && ab.UserId == userId);

                if (existingBookmark == null)
                {
                    
                    return Ok(new ResponseDto<string>
                    {
                        Code = 200,
                        Message = "Bookmark not found, nothing to remove",
                        Data = "Bookmark not found, nothing to remove",
                        Error = string.Empty
                    });
                }

                // Proceed to remove the bookmark if the user has bookmarked the article
                await _bookmarkService.UnbookmarkArticleAsync(articleId, userId);

                // Return a response indicating success
                var response = new ResponseDto<string>
                {
                    Code = 200,
                    Message = "Bookmark removed successfully",
                    Data = "Bookmark removed successfully",
                    Error = string.Empty
                };

                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                var response = new ResponseDto<string>
                {
                    Code = 404, // Not Found
                    Message = ex.Message,
                    Data = string.Empty,
                    Error = ex.Message
                };

                return NotFound(response);
            }
            catch (InvalidOperationException ex)
            {
                var response = new ResponseDto<string>
                {
                    Code = 400, // Bad Request
                    Message = ex.Message,
                    Data = string.Empty,
                    Error = ex.Message
                };

                return BadRequest(response);
            }
            catch (Exception ex)
            {
                var response = new ResponseDto<string>
                {
                    Code = 500, // Internal Server Error
                    Message = "Error removing the bookmark",
                    Data = string.Empty,
                    Error = ex.Message
                };

                return StatusCode(500, response);
            }
        }

    }
}
