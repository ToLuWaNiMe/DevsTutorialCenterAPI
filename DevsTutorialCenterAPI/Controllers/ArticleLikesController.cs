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
    [Authorize]
    [ApiController]
    [Route("api/article-likes")]
    public class ArticlesLikeController : ControllerBase
    {
        private readonly ILikeService _likeService;
        private readonly IRepository _repository;

        public ArticlesLikeController(ILikeService likeService, IRepository repository)
        {
            _likeService = likeService;
            _repository = repository;
        }

        [HttpPost("like/{articleId}")]
        public async Task<IActionResult> LikeArticle(string articleId, [FromBody] LikesByArticleDto dto)
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

                // Proceed to like the article if the user and article exist
                await _likeService.LikeArticleAsync(articleId, userId);

                var response = new ResponseDto<string>
                {
                    Code = 200,
                    Message = "Article liked successfully",
                    Data = "Article liked successfully",
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
                    Message = "Error liking the article",
                    Data = string.Empty,
                    Error = ex.Message
                };

                return StatusCode(500, response);
            }
        }

        [HttpDelete("unlike/{articleId}")]
        public async Task<IActionResult> UnlikeArticle(string articleId, [FromBody] LikesByArticleDto dto)
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

                // Check if the user has liked the article
                var likesQuery = await _repository.GetAllAsync<ArticlesLikes>();
                var existingLike = likesQuery.FirstOrDefault(al => al.ArticleId == articleId && al.UserId == userId);

                if (existingLike == null)
                {
                    return BadRequest("User has not liked the article");
                }

                // Proceed to unlike the article if the user and article exist
                await _likeService.UnlikeArticleAsync(articleId, userId);

                // Return a response indicating success
                var response = new ResponseDto<string>
                {
                    Code = 200,
                    Message = "Article unliked successfully",
                    Data = "Article unliked successfully",
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
                    Message = "Error unliking the article",
                    Data = string.Empty,
                    Error = ex.Message
                };

                return StatusCode(500, response);
            }
        }


    }
}
