using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize]
        [HttpPost("create-article")]
        public async Task<IActionResult> CreateArticle([FromBody]CreateArticleDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdArticle = await _articleService.CreateArticleAsync(model);
            if (createdArticle != null) 
            {
                var response = new ResponseDto<CreateArticleDto>
                {
                    Code = (int)HttpStatusCode.OK,
                    Data = createdArticle,
                    Message = "Article Created Successfully",
                    Error = string.Empty
                };

                return Ok(response);
            }
            else
            {
                var response = new ResponseDto<CreateArticleDto>
                {
                    Code = (int)HttpStatusCode.BadRequest,
                    Data = null,
                    Message = "Failed to create new Article",
                    Error = string.Empty
                };
               
                return BadRequest(response);
            }

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
    }
}
