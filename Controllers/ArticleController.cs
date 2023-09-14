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



        [HttpPatch("{articleId}/report-status")]
        public async Task<IActionResult> SetReportStatus(string articleId, [FromQuery] string status)
        {
            try
            {
                var result = await _articleService.SetArticleReportStatus(articleId, status);

                if (result)
                {
                    return Ok(new ResponseObject
                    {
                        code = 200,
                        message = "Ok",
                        data = "",
                        error = ""
                    });
                }
                
                return BadRequest(new ResponseObject
                {
                     code = 400,
                     message = "Error",
                     data = null,
                     error = "Failed to set report status"
                });
                
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ResponseObject
                {
                    code = 400,
                    message = "Error",
                    data = null,
                    error = ex.Message  
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ResponseObject
                {
                    code = 400,
                    message = "Error",
                    data = null,
                    error = ex.Message  
                });
            }
        }
    }
}
