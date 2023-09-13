using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers
{
    // [Authorize]
    [ApiController]
    [Route("api/articles")]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        private readonly ILogger<ArticleController> _logger;
        public ArticleController(IArticleService articleService, ILogger<ArticleController> logger)
        private readonly IReportArticleService _reportArticleService;

        public ArticleController(IReportArticleService reportArticleService)
        {
            _articleService = articleService;
            _logger = logger;
            _reportArticleService = reportArticleService;
        }

        [HttpPost("report-article")]
        public async Task<ActionResult<ResponseDto<object>>> ReportArticle([FromBody] ReportArticleRequestDto request, string articleId)

        [HttpGet("id")]
        public async Task<ActionResult<ResponseDto<GetAllArticlesDto>>> GetSingleArticle(string articleId)
        {
            try
            // validate to check if model is valid
            if(!ModelState.IsValid)
            {
                var article = await _articleService.GetSingleArticle(articleId);

                if (article == null)
                return BadRequest(new ResponseDto<object>
                {
                    return NotFound($"Article with ID {articleId} not found.");
                    Data = null,
                    Message = "Validation failed",
                    Code = 500,


                }) ;   
                }



            var response = await _reportArticleService.AddArticleReportAsync(request, articleId);

            return Ok(new ResponseDto<object>
                {
                    Data = article,
                    Code = 200,
                Message = "Ok",
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
