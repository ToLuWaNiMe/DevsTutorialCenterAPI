using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DevsTutorialCenterAPI.Data.Entities;

namespace DevsTutorialCenterAPI.Controllers
{
    [ApiController]
    [Route("api/articles")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly ILogger<ArticleController> _logger;
        
        private readonly IReportArticleService _reportArticleService;

        public ArticleController(IReportArticleService reportArticleService, IArticleService articleService, ILogger<ArticleController> logger)
        {
            _articleService = articleService;
            _logger = logger;
            _reportArticleService = reportArticleService;
        }
        
        [HttpGet("")]
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

                return Ok(new ResponseDto<GetSingleArticleDto>
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

        [HttpPost("report-article")]
        public async Task<ActionResult<ResponseDto<object>>> ReportArticle([FromBody] ReportArticleRequestDto request, string articleId)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(new ResponseDto<object>
                {
                    Data = null,
                    Message = "Validation failed",
                    Code = 500,
                });

                

            }


            var response = await _reportArticleService.AddArticleReportAsync(request, articleId);

            return Ok(new ResponseDto<object>
            {
                Data = response,
                Code = 200,
                Message = "Ok",
                Error = ""
            });
        }

    }
}
