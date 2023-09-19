using DevsTutorialCenterAPI.Models.DTOs;
using DevsTutorialCenterAPI.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace DevsTutorialCenterAPI.Controllers;

[Route("api/comment-likes")]
[ApiController]
public class CommentsLikesController : ControllerBase
{
    private readonly ICommentsLikesService _commentsLikesService;

    public CommentsLikesController(ICommentsLikesService commentsLikesService)
    {
        _commentsLikesService = commentsLikesService;
    }

    // [Authorize]
    [HttpGet("{commentId}")]
    public async Task<IActionResult> GetLikesByCommentId([FromRoute] string commentId)
    {
        try
        {
            var likes = await _commentsLikesService.GetLikesByCommentsAsync(commentId);

            var response = new ResponseDto<List<LikesByCommentsDto>>();

            if (likes.Count == 0)
            {
                response.Code = 404;
                response.Message = "Not Found";
                response.Data = likes;
                response.Error = "Likes not found for the specified comment.";
                return NotFound(response);
            }

            response.Code = 200;
            response.Message = "OK";
            response.Data = likes;
            response.Error = "";

            return Ok(response);
        }
        catch (Exception ex)
        {
            var errorResponse = new ResponseDto<object>
            {
                Code = 500,
                Message = "Internal Server Error",
                Data = null,
                Error = ex.Message
            };

            return StatusCode(500, errorResponse);
        }
    }
}