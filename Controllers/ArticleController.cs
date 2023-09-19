using System.Net;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers;

[ApiController]
[Route("api/articles")]
public class ArticleController : ControllerBase
{
    private readonly IArticleService _articleService;
    private readonly ILogger<ArticleController> _logger;
    private readonly IReportArticleService _reportArticleService;

    public ArticleController(IReportArticleService reportArticleService, IArticleService articleService,
        ILogger<ArticleController> logger)
    {
        _articleService = articleService;
        _logger = logger;
        _reportArticleService = reportArticleService;
    }

    [Authorize]
    [HttpPost("")]
    public async Task<IActionResult> CreateArticle([FromBody] CreateArticleDto model)
    {
        if (!ModelState.IsValid)
        {
            var modelStateErrors = ModelState.GetError();
            var response = new ResponseDto<string>
            {
                Code = (int)HttpStatusCode.BadRequest,
                Data = modelStateErrors,
                Message = "Validation Failed",
                Error = "One or more validation errors occurred."
            };
            return BadRequest(response);
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

            if (article == null) return NotFound($"Article with ID {articleId} not found.");

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

    [Authorize]
    [HttpDelete("")]
        public async Task<IActionResult> DeleteArticle(string articleId)
        {
            try
            {
                var result = await _articleService.DeleteArticleAsync(articleId);

                if (result)
                {
                    var response = new ResponseDto<bool>
                    {
                        Code = (int)HttpStatusCode.NoContent,
                        Data = true,
                        Message = "Article Deleted Successfully",
                        Error = string.Empty
                    };

                    return Ok(response); 
                }
                else
                {
                    var response = new ResponseDto<bool>
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Data = false,
                        Message = "Failed to Delete Article",
                        Error = string.Empty
                    };

                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }


    [HttpPost("{articleId}/report-article")]
    public async Task<ActionResult<ResponseDto<object>>> ReportArticle([FromBody] ReportArticleRequestDto request,
        string articleId)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResponseDto<object>
            {
                Data = null,
                Message = "Validation failed",
                Code = 500
            });


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