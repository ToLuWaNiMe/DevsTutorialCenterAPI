using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevsTutorialCenterAPI.Controllers
{
    [Route("articles")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly ILogger<ArticleController> _logger;
        public ArticleController(IArticleService articleService, ILogger<ArticleController> logger)
        {
            _articleService = articleService;
            _logger = logger;
        }


        [HttpGet("id")]
        public async Task<ActionResult<ResponseDto<GetAllArticlesDto>>> GetSingleArticle(string articleId)
        {
            try
            {
                var article = await _articleService.GetSingleArticle(articleId);

                if (article == null)
                {
                    return NotFound($"Article with ID {articleId} not found.");
                }

                return Ok(new ResponseDto<GetAllArticlesDto>
                {
                    Data = article,
                    Code = 200,
                    Message = "OK",
                    Error = ""
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpDelete("delete-article")]
        public async Task<IActionResult> DeleteArticle(string articleId)
        {
            try
            {
                var result = await _articleService.DeleteArticleAsync(articleId);

                if (result)
                {
                    var response = new ResponseDto<bool>
                    {
                        Code = (int)HttpStatusCode.NoContent,
                        Data = true,
                        Message = "Article Deleted Successfully",
                        Error = string.Empty
                    };

                    return Ok(response); 
                }
                else
                {
                    var response = new ResponseDto<bool>
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Data = false,
                        Message = "Failed to Delete Article",
                        Error = string.Empty
                    };

                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }


    }
}
