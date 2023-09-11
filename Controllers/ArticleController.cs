using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
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

        [HttpGet("all")]
        public async Task<ActionResult<ResponseDto<IEnumerable<GetAllArticlesDto>>>> GetAllArticles(int pageNum, int pageSize=10)
        {
            try
            {
                var articles = await _articleService.GetAllArticles();

                var skipAmount = (pageNum - 1) * pageSize;

                var paginatedArticles = articles.Skip(skipAmount).Take(pageSize);

                if (articles == null)
                {
                    return NotFound();
                }
                return Ok(new ResponseDto<IEnumerable<GetAllArticlesDto>>
                {
                    Data = paginatedArticles,
                    Code = 200,
                    Message = "OK",
                    Error = ""
                }) ;
            }
            catch (Exception ex)
            {
               _logger.LogError($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
            
        }
    }
}
