using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

         [HttpGet("get-all-articles")]
        public async Task<ActionResult> GetAllArticles([FromQuery] FilterArticleDto filters)
        {
            try
            {
                var articles = await _articleService.GetAllArticles(filters);

                return Ok(new ResponseDto<PaginatorResponseDto<IEnumerable<GetAllArticlesDto>>>
                {
                    Data = articles,
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

        [HttpGet("{articleId}")]
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