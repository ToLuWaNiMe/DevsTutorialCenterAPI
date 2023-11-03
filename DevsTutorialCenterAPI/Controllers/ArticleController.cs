using System.Net;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Models.Enums;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Services.Implementations;
using DevsTutorialCenterAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers;

[Authorize]
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


    

    [HttpPost("create-article")]
    public async Task<IActionResult> CreateArticle([FromBody] CreateArticleDto model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        if (string.IsNullOrWhiteSpace(model.Tag))
        {
            ModelState.AddModelError("Tag", "Article must have at least one tag.");
            return BadRequest(ModelState);
        }

        var createdArticle = await _articleService.CreateArticleAsync(model);

        return Ok(new ResponseDto<CreateArticleDto>
        {
            Data = createdArticle,
            Code = 200,
            Message = "OK",
            Error = ""
        });

    }

    [AllowAnonymous]
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

    [AllowAnonymous]
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


    [HttpDelete("delete-article/{Id}")]
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


    [HttpPatch("{articleId}/report-status")]
    public async Task<IActionResult> SetReportStatus(string articleId, [FromQuery] string status)
    {
        try
        {
            var result = await _articleService.SetArticleReportStatus(articleId, status.ToLower());

            if (result)
            {
                return Ok(new ResponseDto<object>
                {
                    Data = result,
                    Code = 200,
                    Message = "Ok",
                    Error = ""
                });
            }

            return BadRequest(new ResponseDto<object>
            {
                Data = result,
                Code = 400,
                Message = "Status failed",
                Error = "Set report status failed"
            });

        }
        
        catch (Exception ex)
        {
            return BadRequest(new ResponseDto<object>
            {
                Data = null,
                Code = 400,
                Message = "Status failed",
                Error = ex.Message
            });
        }
    }

    [HttpGet("{articleId}/get-likes")]
    public async Task<IActionResult> GetLikesByArticleAsync([FromRoute] string articleId)
    {
        var response = new ResponseDto<List<LikesByArticleDto>>();

        try
        {
            var likes = await _articleService.GetLikesByArticleAsync(articleId);
            if (!likes.Any())
            {
                response.Code = 404;
                response.Message = "Not Found";
                response.Data = likes;
                response.Error = " ";
                return NotFound(response);
            }

            response.Code = 200;
            response.Message = "OK";
            response.Data = likes;
            response.Error = " ";
        }
        catch (Exception ex)
        {
            response.Code = 500;
            response.Message = "Internal Server Error";
            response.Data = null;
            response.Error = ex.Message;
            return BadRequest(response);
        }

        return Ok(response);
    }

    [HttpPost("fetch-authors-stats")]
    public async Task<IActionResult> GetAuthorsStats([FromBody] FetchAuthorsStatsDto fetchAuthorsStatsDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new ResponseDto<object>
            {
                Error = ModelState.GetError(),
                Message = "author list empty",
                Code = 400,
            });

        var result = await _articleService.GetAuthorStatsAsync(fetchAuthorsStatsDto);

        return Ok(new ResponseDto<AuthorsStatsDto>
        {
            Code = 200,
            Data = result,
            Message = "Successful"
        });
    }
}