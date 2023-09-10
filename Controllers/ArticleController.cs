using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers
{
    [Route("article")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private readonly IArticleService _articleService;
        public ArticleController(IArticleService articleService) 
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSingleArticle(string articleid)
        {
            try
            {
                var article = await _articleService.GetSingleArticle(articleid);

                if (article == null)
                {
                    return NotFound();
                }

                return Ok(article);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
    }
}
