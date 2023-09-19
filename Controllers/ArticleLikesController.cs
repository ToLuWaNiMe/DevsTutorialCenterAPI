using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers;

[Route("api/article-likes")]
[ApiController]
public class ArticleLikesController : ControllerBase
{
    private readonly ILikesService _likesService;

    public ArticleLikesController(ILikesService likesService)
    {
        _likesService = likesService;
    }

    [Authorize]
    [HttpGet("{articleId}")]
    public async Task<IActionResult> GetLikesByArticleAsync([FromRoute] string articleId)
    {
        var response = new ResponseDto<List<LikesByArticleDto>>();

        try
        {
            var likes = await _likesService.GetLikesByArticleAsync(articleId);
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
}