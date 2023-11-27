using System.Net;
using System.Security.Claims;
using System.Xml.Linq;
using DevsTutorialCenterAPI.Data.Entities;
using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Models.Enums;
using DevsTutorialCenterAPI.Services.Abstractions;
using DevsTutorialCenterAPI.Services.Implementations;
using DevsTutorialCenterAPI.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers;

//[Authorize]
[ApiController]
[Route("api/articles")]
public class ArticleController : ControllerBase
{
    private readonly IArticleService _articleService;
    private readonly ILogger<ArticleController> _logger;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly IReportArticleService _reportArticleService;
    private readonly IArticleApprovalService _articleApprovalService;

    public ArticleController(IReportArticleService reportArticleService, IArticleService articleService,
        ILogger<ArticleController> logger, SignInManager<AppUser> signInManager, IArticleApprovalService articleApprovalService)
    {
        _articleService = articleService;
        _logger = logger;
        _signInManager = signInManager;
        _reportArticleService = reportArticleService;
        _articleApprovalService = articleApprovalService;
    }



    ////DONE
    //[HttpPost("create-article")]
    //public async Task<IActionResult> CreateArticle([FromBody] CreateArticleDto model)
    //{


    //    //string[] allowedTags = { "JAVA", ".NET", "NODE" };
    //    //if (!allowedTags.Contains(model.TagId, StringComparer.OrdinalIgnoreCase))
    //    //    return BadRequest(new ResponseDto<CreateArticleDto>
    //    //    {
    //    //        Data = null,
    //    //        Code = 500,
    //    //        Message = "Artcile Creation failed",
    //    //        Error = "Invalid tag. Tag must either one of: JAVA, .NET, NODE."
    //    //    });

    //    if (!ModelState.IsValid) return BadRequest (new ResponseDto<CreateArticleDto>
    //    {
    //        Data = null,
    //        Code = 500,
    //        Message = "Artcile Creation failed",
    //        Error = "Invalid Data"
    //    });

    //    if (string.IsNullOrWhiteSpace(model.TagId))
    //    {
    //        //ModelState.AddModelError("Tag", "Article must have at least one tag.");
    //        return BadRequest(new ResponseDto<CreateArticleDto>
    //        {
    //            Data = null,
    //            Code = 500,
    //            Message = "Artcile Creation failed",
    //            Error = "Article must have at least one tag"
    //        });
    //    }

    //    var createdArticle = await _articleService.CreateArticleAsync(model);

    //    return Ok(new ResponseDto<CreateArticleDto>
    //    {
    //        Data = createdArticle,
    //        Code = 200,
    //        Message = "OK",
    //        Error = ""
    //    });

    //}
    //[Authorize]
    [HttpPost("create-article")]
    public async Task<IActionResult> CreateArticle2([FromBody] CreateArticleDto2 model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ResponseDto<CreateArticleDtoReturn>
            {
                Data = null,
                Code = 500,
                Message = "Article Creation failed",
                Error = "Invalid Data",
                
            });
        }

        if (string.IsNullOrWhiteSpace(model.TagId))
        {
            return BadRequest(new ResponseDto<CreateArticleDtoReturn>
            {
                Data = null,
                Code = 500,
                Message = "Article Creation failed",
                Error = "Article must have at least one tag"
            });
        }

        try
        {
            

           
            var createdArticle = await _articleService.CreateArticleAsync2(model);

            return Ok(new ResponseDto<CreateArticleDtoReturn>
            {
                Data = createdArticle,
                Code = 200,
                Message = "OK",
                Error = "",
                IsSuccessful = true
            });
        }
        catch (Exception ex)
        {
            // Log the error and return an appropriate response
            _logger.LogError($"Error: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
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
                Error = "",
                IsSuccessful = true
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }

   // [AllowAnonymous]
    [HttpGet("get-bookmarked-articles")]
    //[Authorize]
    public async Task<ActionResult> GetBookmarkedArticles([FromQuery] string userId)
    {
        try
        {
            var bookmarkedArticles = await _articleService.GetBookmarkedArticles(userId);

            return Ok(new ResponseDto<IEnumerable<GetAllArticlesDto>>
            {
                Data = bookmarkedArticles,
                Code = 200,
                Message = "OK",
                Error = "",
                IsSuccessful = true
            });
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error: {ex.Message}");
            return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }


    [AllowAnonymous]
    [HttpGet("all-articles")]
    public async Task<ActionResult<IEnumerable<Article>>> GetAllArticle()
    {
        var articles = await _articleService.GetAllArticle();
        if(articles == null)
        {
            return BadRequest(new ResponseDto<IEnumerable<Article>>
            {
                Code = 500,
                Data = null,
                Message = "Articles not retrieved",
                Error = "Failed"



            }); 
        }

        return Ok(new ResponseDto<IEnumerable<Article>>
        {
            Code=200,
            Data = articles,
            Message = "Articles successfully retrieved",
            Error = "",
            IsSuccessful = true

        });
    }

    //DONE
    [AllowAnonymous]
    [HttpGet("{articleId}/is-bookmarked")]
    public async Task<ActionResult<bool>> IsArticleBookmarked(string articleId)
    {

        string currentUserId = GetCurrentUserId();


        bool isBookmarked = await _articleService.IsArticleBookmarkedByUser(articleId, currentUserId);

        return Ok(isBookmarked);
    }

    
    private string GetCurrentUserId()

    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return userId;
    }


    //DONE
    [AllowAnonymous]
    [HttpGet("get-single-article/{articleId}")]
    //[Authorize]
    public async Task<ActionResult<ResponseDto<GetSingleArticleDto>>> GetSingleArticle(string articleId)
    {
        var user = await _signInManager.UserManager.GetUserAsync(User);

        


        if (string.IsNullOrEmpty(articleId))
        {
            return BadRequest(new ResponseDto<GetSingleArticleDto>
            {
                Data = null,
                Code = (int)HttpStatusCode.BadRequest,
                Message = "Bad Request",
                Error = "Invalid articleId"
            });
        }

        var article = await _articleService.GetSingleArticle(articleId, user.Id);

        if (article == null)
        {
            return BadRequest(new ResponseDto<GetSingleArticleDto>
            {
                Data = null,
                Code = (int)HttpStatusCode.NotFound,
                Message = "Not Found",
                Error = "Article not found"
            });
        }

        return Ok(new ResponseDto<GetSingleArticleDto>
        {
            Data = article,
            Code = (int)HttpStatusCode.OK,
            Message = "OK",
            Error = "",
            IsSuccessful = true
        });
    }


    //DONE
    [HttpPut("update-article/{articleId}")]
    public async Task<IActionResult> UpdateArticle(string articleId, [FromBody] UpdateArticleDto updatedArticle)
    {
        if (updatedArticle == null)
        {
            return BadRequest(new ResponseDto<UpdateArticleDto>
            {
                Code = (int)HttpStatusCode.BadRequest,
                Data = null,
                Message = "Invalid or empty article data",
                Error = "The provided article data is invalid or empty."
            });
        }

        var updateResult = await _articleService.UpdateArticleAsync(articleId, updatedArticle);

        if (updateResult == null)
        {
            return BadRequest(new ResponseDto<UpdateArticleDto>
            {
                Code = (int)HttpStatusCode.BadRequest,
                Data = null,
                Message = "Failed to update the article",
                Error = "An error occurred while trying to update the article."
            });
        }

        return Ok(new ResponseDto<UpdateArticleDto>
        {
            Code = (int)HttpStatusCode.OK,
            Data = updatedArticle,
            Message = "Article updated successfully",
            Error = string.Empty,
            IsSuccessful = true
        });
    }


    [HttpDelete("delete-article/{articleId}")]
   // [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> DeleteArticle(string articleId)
        {
            try
            {
                var result = await _articleService.SoftDeleteArticle(articleId);

                if (result != null)
                {
                    var response = new ResponseDto<object>
                    {
                        Code = 200,
                        Data = result,
                        Message = "Article Deleted Successfully",
                        Error = string.Empty,
                        IsSuccessful = true
                    };

                    return Ok(response); 
                }
                else
                {
                    var response = new ResponseDto<object>
                    {
                        Code = (int)HttpStatusCode.BadRequest,
                        Data = null,
                        Message = "Failed to Delete Article",
                        Error = "Failed",
                        
                        
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


    //[HttpPost("{articleId}/report-article")]
    //public async Task<ActionResult<ResponseDto<object>>> ReportArticle([FromBody] ReportArticleRequestDto request,
    //    string articleId)
    //{
    //    if (!ModelState.IsValid)
    //        return BadRequest(new ResponseDto<object>
    //        {
    //            Data = null,
    //            Message = "Validation failed",
    //            Code = 500
    //        });


    //    var response = await _reportArticleService.AddArticleReportAsync(request, articleId);

    //    return Ok(new ResponseDto<object>
    //    {
    //        Data = response,
    //        Code = 200,
    //        Message = "Ok",
    //        Error = ""
    //    });


       
    //}


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

    //[HttpGet("{articleId}/get-likes")]
    //public async Task<IActionResult> GetLikesByArticleAsync([FromRoute] string articleId)
    //{
    //    var response = new ResponseDto<List<LikesByArticleDto>>();

    //    try
    //    {
    //        var likes = await _articleService.GetLikesByArticleAsync(articleId);
    //        if (!likes.Any())
    //        {
    //            response.Code = 404;
    //            response.Message = "Not Found";
    //            response.Data = likes;
    //            response.Error = " ";
    //            return NotFound(response);
    //        }

    //        response.Code = 200;
    //        response.Message = "OK";
    //        response.Data = likes;
    //        response.Error = " ";
    //    }
    //    catch (Exception ex)
    //    {
    //        response.Code = 500;
    //        response.Message = "Internal Server Error";
    //        response.Data = null;
    //        response.Error = ex.Message;
    //        return BadRequest(response);
    //    }

    //    return Ok(response);
    //}

    //[HttpPost("fetch-authors-stats")]
    //public async Task<IActionResult> GetAuthorsStats([FromBody] FetchAuthorsStatsDto fetchAuthorsStatsDto)
    //{
    //    if (!ModelState.IsValid)
    //        return BadRequest(new ResponseDto<object>
    //        {
    //            Error = ModelState.GetError(),
    //            Message = "author list empty",
    //            Code = 400,
    //        });

    //    var result = await _articleService.GetAuthorStatsAsync(fetchAuthorsStatsDto);

    //    return Ok(new ResponseDto<AuthorsStatsDto>
    //    {
    //        Code = 200,
    //        Data = result,
    //        Message = "Successful"
    //    });
    //}

    //[Authorize(Roles = "EDITOR")]
    //[HttpPost("approve-article/{articleId}")]
    //public async Task<IActionResult> ApproveArticle(string articleId)
    //{

    //    var result = await _articleApprovalService.ApprovalArticleById(articleId);

    //    if (result != null)
    //    {
    //        return Ok(new ResponseDto<ArticleApproval>
    //        {
    //            Data = result,
    //            Code = 200,
    //            Message = "Article Approved Successfully",
    //            Error = string.Empty,
    //            IsSuccessful = true
    //        });
    //    }
    //    else
    //    {
    //        return BadRequest(new ResponseDto<ArticleApproval>
    //        {
    //            Data = null,
    //            Code = 400,
    //            Message = "Failed to Meet Approval Requirement",
    //            Error = string.Empty
    //        });
    //    }
    //}

    //[Authorize(Roles = "EDITOR")]
    [HttpPost("publish-article/{articleId}")]
    public async Task<IActionResult> PublishArticle(string articleId)
    {
       
        var result = await _articleApprovalService.PublishedArticle(articleId);

        if (result != null)
        {
            return Ok(new ResponseDto<ArticleApproval>
            {
                Data = result,
                Code = 200,
                Message = "Article Published Successfully",
                Error = string.Empty,
                IsSuccessful = true
            });
        }
        else
        {
            return BadRequest(new ResponseDto<ArticleApproval>
            {
                Data = null,
                Code = 400,
                Message = "Failed to Publish Article",
                Error = string.Empty
            });
        }
    }

    //[Authorize(Roles = "EDITOR")]
    [HttpPost("review-article/{articleId}")]
    public async Task<IActionResult> ReviewArticle(string articleId)
    {
        var result = await _articleApprovalService.ReviewArticle(articleId);

        if (result != null)
        {
            return Ok(new ResponseDto<ArticleApproval>
            {
                Data = result,
                Code = 200,
                Message = "Article Reviewed Successfully",
                Error = string.Empty,
                IsSuccessful = true
            });
        }
        else
        {
            return BadRequest(new ResponseDto<ArticleApproval>
            {
                Data = null,
                Code = 400,
                Message = "Failed to Review Article",
                Error = string.Empty
            });
        }
    }

    [HttpPost("reject-article/{articleId}")]
    //[Authorize(Roles = "EDITOR")]
    public async Task<IActionResult> RejectArticle(string articleId)
    {
        var result = await _articleApprovalService.RejectArticle(articleId);

        if (result != null)
        {
            return Ok(new ResponseDto<ArticleApproval>
            {
                Data = result,
                Code = 200,
                Message = "Article rejected successfully",
                Error = string.Empty,
                IsSuccessful = true
            });
        }
        else
        {
            return BadRequest(new ResponseDto<ArticleApproval>
            {
                Data = null,
                Code = 400,
                Message = "Failed to Reject Article",
                Error = string.Empty
            });
        }
        

    }

    [HttpGet("get-reported-articles")]
    //[Authorize(Roles = "EDITOR, ADMIN")]
    public async Task<IActionResult> GetReportedArticles()
    {
        var result = await _reportArticleService.GetReportedArticlesAsync();

        if (result != null)
        {
            return Ok(new ResponseDto<List<GetReportedArticleDTO>>
            {
                Data = result,
                Code = 200,
                Message = "Reported articles retrieved successfully",
                Error = string.Empty,
                IsSuccessful = true
            });
        }
        else
        {
            return BadRequest(new ResponseDto<List<GetReportedArticleDTO>>
            {
                Data = null,
                Code = 400,
                Message = "Failed to retrieve reported articles",
                Error = string.Empty
            });
        }
    }

    [HttpGet("get-reported-authors")]
    //[Authorize(Roles = "EDITOR, ADMIN")]
    public async Task<IActionResult> GetReportedAuthors()
    {
        var result = await _reportArticleService.GetReportedAuthorsAsync();

        if (result != null)
        {
            return Ok(new ResponseDto<List<GetReportedAuthorsDTO>>
            {
                Data = result,
                Code = 200,
                Message = "Reported articles retrieved successfully",
                Error = string.Empty,
                IsSuccessful = true
            });
        }
        else
        {
            return BadRequest(new ResponseDto<List<GetReportedAuthorsDTO>>
            {
                Data = null,
                Code = 400,
                Message = "Failed to retrieve reported articles",
                Error = string.Empty
            });
        }
    }

    [HttpGet("fetch-articles-by-approval-status/{number}")]
    //[Authorize(Roles = "EDITOR, ADMIN")]
    public async Task<IActionResult> FetchArticlesByApprovalStatus(int number)
    {
        var result = await _articleApprovalService.FetchArticleStatusListAsync(number);

        if (result != null)
        {
            return Ok(new ResponseDto<List<GetSingleArticleDto>>
            {
                Data = result,
                Code = 200,
                Message = "Articles retrieved successfully",
                Error = string.Empty,
                IsSuccessful = true
            });
        }
        else
        {
            return BadRequest(new ResponseDto<List<GetSingleArticleDto>>
            {
                Data = null,
                Code = 400,
                Message = "Failed to retrieve article",
                Error = string.Empty
            });
        }

    }

    [HttpPost("report-article")]
    public async Task<IActionResult> ReportArticle([FromBody] CreateReportDTO createReportDTO)
    {
        var user = await _signInManager.UserManager.GetUserAsync(User);

        var result = await _reportArticleService.ReportArticle(createReportDTO, user.Id);

        if (result != null)
        {
            return Ok(new ResponseDto<ReportedArticle>
            {
                Data = result,
                Code = 200,
                Message = "Article reported  successfully",
                Error = string.Empty,
                IsSuccessful = true
            });
        }
        else
        {
            return BadRequest(new ResponseDto<ReportedArticle>
            {
                Data = null,
                Code = 400,
                Message = "Failed to report article",
                Error = string.Empty
            });
        }
    }


}