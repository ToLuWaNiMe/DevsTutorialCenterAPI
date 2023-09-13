using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Utilities;
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

        [HttpGet(" ")]
        public async Task<ActionResult<PaginatorResponseDto<IEnumerable<GetAllArticlesDto>>>> GetAllArticles(int pageNum, int pageSize = 10)
        {
            try
            {
                var articles = await _articleService.GetAllArticles();

                var paginatorResponse = Helper.Paginate(articles, pageNum, pageSize);

                if (paginatorResponse == null)
                {
                    return NotFound("Invalid entry!");
                }
                return Ok(new ResponseDto<PaginatorResponseDto<IEnumerable<GetAllArticlesDto>>>
                {
                    Data = paginatorResponse,
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
