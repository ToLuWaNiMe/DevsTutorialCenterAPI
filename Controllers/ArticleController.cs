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

        [HttpGet("")]
        public async Task<IActionResult> GetAllArticles()
        {
            try
            {
                var articles = await _articleService.GetAllArticles();

                if (articles == null)
                {
                    return NotFound();
                }
                return Ok(articles);
            }
            catch (Exception ex)
            {
               _logger.LogError($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
            
        }
    }
}
