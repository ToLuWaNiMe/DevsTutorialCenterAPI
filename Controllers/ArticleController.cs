using DevsTutorialCenterAPI.Data.Repositories;
using DevsTutorialCenterAPI.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers
{
    [Route("articles")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleRepository _articleRepository;
        private readonly ILogger<ArticleController> _logger;    
        public ArticleController(IArticleRepository articleRepository, ILogger<ArticleController> logger) 
        {
            _articleRepository = articleRepository;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllArticles()
        {
            try
            {
                var articles = await _articleRepository.GetAll();

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
